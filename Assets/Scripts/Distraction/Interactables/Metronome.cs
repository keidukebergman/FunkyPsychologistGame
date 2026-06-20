using UnityEngine;

public class Metronome : MonoBehaviour, Interactable
{
    public bool isOneShot { get => true; set => throw new System.NotImplementedException(); }
    [SerializeField] private float stimulationValue = 0.1f;

    [SerializeField] private GameObject pin = null;
    [SerializeField] private float speed = 90f;
    [SerializeField] private MetronomeState metroState = MetronomeState.Idle;

    [SerializeField] private float maxTilt = 30;
    [SerializeField] float dir = 1;
    enum MetronomeState
    {
        Idle,
        LeftState,
        RightState
    }

    public void OnInteract(RaycastHit raycastHit)
    {
        if (Mathf.Abs(speed) > 1)
        {
            speed = 0;
        }
        else
        {
            speed = 140;
        }
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, stimulationValue);
    }

    public void Update()
    {
        pin.transform.Rotate(Vector3.right, speed * dir * Time.deltaTime);
        if (dir == -1 && Vector3.Dot(transform.forward, -pin.transform.up) > 0.6)
        {
            SwitchSide();
        }
        if (dir == 1 && Vector3.Dot(transform.forward, -pin.transform.up) < -0.6)
        {
            SwitchSide();
        }
    }

    private void SwitchSide()
    {
        GetComponent<AudioSource>().pitch = Random.Range(1.9f, 2.1f);
        GetComponent<AudioSource>().Play();
        dir *= -1;
    }
}
