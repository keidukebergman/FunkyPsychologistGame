using UnityEngine;

public class CameraZoomControls : MonoBehaviour, Interactable
{
    [SerializeField] private float minimumFov, maximumFov = 70;
    [SerializeField] private float fovZoomSpeed = 20;
    float fovZoom = 55;
    [SerializeField] private float tvStimulation = 0.2f;

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

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Interactable interactable = hit.transform.gameObject.GetComponent<InteractableTV>();
                if (interactable != null)
                {
                    DistractionManager.instance.OnInteractWithPersistentInteractable(interactable, tvStimulation * ((1 + (maximumFov - fovZoom)) / minimumFov) * Time.deltaTime);
                }
            }

        GetComponent<Camera>().fieldOfView = Mathf.MoveTowards(GetComponent<Camera>().fieldOfView, fovZoom, fovZoomSpeed * Time.deltaTime);
    }
}
