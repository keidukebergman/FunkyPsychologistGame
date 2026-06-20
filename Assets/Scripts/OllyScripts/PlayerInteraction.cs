using UnityEngine;
using static Unity.Cinemachine.CinemachineOrbitalTransposer;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private LayerMask interactLayers = ~0; // Everything by default
    [SerializeField] private PersistentInteractable persistentInteractable;
    [SerializeField] private InteractableBobbleComponent bobbleComponent;
    [SerializeField] private InteractablePiano interactablePiano;
    [SerializeField] private ClickableTelephone clickableTelephone;
    [SerializeField] private ClickableTVRemote clickableTVRemote;
    private PersistentInteractable currentPersistent;
    [SerializeField] private InteractableFidgetSpinner interactableFidgetSpinner;

    bool holding = false;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayers))
            {
                currentPersistent = hit.collider.GetComponentInParent<PersistentInteractable>();

                if (currentPersistent != null)
                {
                    holding = true;
                }

                MonoBehaviour[] scripts = hit.collider.GetComponentsInParent<MonoBehaviour>();

                foreach (var script in scripts)
                {
                    Debug.Log(script.GetType().Name);
                }

                ClickableInteractable interactable =
                    hit.collider.GetComponentInParent<ClickableInteractable>();

                Debug.Log("Found interactable: " +
                    (interactable == null ? "NULL" : interactable.GetType().Name));

                if (interactable != null)
                {
                    interactable.OnInteract(hit);
                    Debug.Log("Called " + interactable.GetType().Name);
                }
            }
            /*if (Input.GetMouseButtonDown(0))
            {
                holding = true;
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayers))
                {
                    Debug.Log("Hit: " + hit.collider.name);

                    ClickableInteractable interactable =
                        hit.collider.GetComponentInParent<ClickableInteractable>();

                    if (interactable != null)
                    {

                        if (interactable.name == "Phone")
                        {
                            clickableTelephone.OnInteract(hit);
                            Debug.Log("Interacted with: " + interactable.name);
                        }
                        if (interactable.name == "TestBobble")
                        {
                            bobbleComponent.OnInteract(hit);
                            Debug.Log("Interacted with: " + interactable.name);
                        }
                        if (interactable.name == "FidgetSpinnar")
                        {
                            interactableFidgetSpinner.OnInteract(hit);
                            Debug.Log("Interacted with: " + interactable.name);
                        }
                        if (interactable.name == "InteractivePiano")
                        {
                            interactablePiano.OnInteract(hit);
                            Debug.Log("Interacted with: " + interactable.name);
                        }
                        if (interactable.name == "Remote (1)")
                        {
                            clickableTVRemote.OnInteract(hit);
                            Debug.Log("Interacted with: " + interactable.name);

        }
            if (Input.GetMouseButtonUp(0))
                {
                    holding = false;
                }

                if (holding)
                {
                    persistentInteractable.OnInteract(new RaycastHit());
                }
     */


            if (Input.GetMouseButtonUp(0))
            {
                holding = false;
                currentPersistent = null;
            }

            if (holding && currentPersistent != null)
            {
                currentPersistent.OnInteract(new RaycastHit());
            }

        }
    }
}

        /*
         *    if (interactable != null)
                {
                    Debug.Log("Interacted with: " + interactable.name);
                    if (interactable.name == "Phone")
                    {
                        clickableTelephone.OnInteract(new RaycastHit());
                        interactable.OnInteract(hit);
                }
            }*/