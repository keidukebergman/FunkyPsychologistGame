using UnityEngine;

public class CameraZoomControls : MonoBehaviour, Interactable
{
    [SerializeField] private float minimumFov, maximumFov = 70;
    [SerializeField] private float fovZoomSpeed = 20;
    float fovZoom = 55;

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
            fovZoom = maximumFov;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKey(KeyCode.L))
        {
            fovZoom = minimumFov;
        }

        if (fovZoom == minimumFov)
        {
            Debug.Log("Raycast firing");
            RaycastHit hit;
            //TV Logic
        }
        GetComponent<Camera>().fieldOfView = Mathf.MoveTowards(GetComponent<Camera>().fieldOfView, fovZoom, fovZoomSpeed * Time.deltaTime);
    }
}
