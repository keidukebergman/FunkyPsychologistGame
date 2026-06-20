using UnityEngine;

public class NewtonCradle : MonoBehaviour, Interactable
{
    public bool isOneShot { get => true; set => throw new System.NotImplementedException(); }
    [SerializeField] private float stimulationValue = 0.1f;

    [SerializeField] private float maxSwingHeight = 120;
    [SerializeField] private GameObject leftBall = null;
    [SerializeField] private GameObject rightBall = null;
    [SerializeField] private float speed = 0.4f;
    [SerializeField] float energyLossCoefficent = 0.9f;
    [SerializeField] private BallState ballState = BallState.LeftBall;
    [SerializeField] private GameObject directionReference = null;

    enum BallState
    {
        Idle,
        LeftBall,
        RightBall
    }
    
    public void OnInteract(RaycastHit raycastHit)
    {
        if(ballState == BallState.Idle)
        {
            speed = -100;
            ballState = BallState.LeftBall;
        }
        else
        {
            return;
        }
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, stimulationValue);
    }

    public void Update()
    {
        if (ballState == BallState.Idle)
            return;

        switch (ballState)
        {
            case BallState.LeftBall:
                leftBall.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
                speed += 9.82f * 9.82f * Time.deltaTime;
                if (speed > 0 && Vector3.Dot(leftBall.transform.up, directionReference.transform.forward) < 0)
                {
                    OnBallHit();
                    leftBall.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 180));
                }
                break;

            case BallState.RightBall:
                
                rightBall.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
                speed -= 9.82f * 9.82f * Time.deltaTime;
                if (speed < 0 && Vector3.Dot(rightBall.transform.up, directionReference.transform.forward) > 0)
                {
                    OnBallHit();
                    leftBall.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 180));
                }
                break;
        }
    }

    private void OnBallHit()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        GetComponent<AudioSource>().Play();
        print("Ball is hit!");
        speed *= energyLossCoefficent;
        if (Mathf.Abs(speed) < 9)
        {
            ballState = BallState.Idle;
            return;
        }
        if(ballState == BallState.LeftBall)
        {
            ballState = BallState.RightBall;
            rightBall.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
        else if(ballState == BallState.RightBall)
        {
            ballState = BallState.LeftBall;
            rightBall.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
    }
}
