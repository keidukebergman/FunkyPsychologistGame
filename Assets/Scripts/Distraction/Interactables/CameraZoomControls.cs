using UnityEngine;

public class CameraZoomControls : MonoBehaviour, Interactable
{
    [SerializeField] private float minimumFov, maximumFov = 70;
    [SerializeField] private float fovZoomSpeed = 20;

    public bool isOneShot { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void OnInteract(RaycastHit raycastHit)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKey(KeyCode.K))
        {
            GetComponent<Camera>().fieldOfView = maximumFov;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKey(KeyCode.L))
        {
            GetComponent<Camera>().fieldOfView = minimumFov;
        }

        if (GetComponent<Camera>().fieldOfView == minimumFov)
        {
            Debug.Log("Raycast firing");
            RaycastHit hit;
            //TV Logic
        }
    }
}
