using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private LayerMask interactLayers = ~0; // Everything by default

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayers))
        {
            Debug.Log("Hit: " + hit.collider.name);

            ClickableInteractable interactable =
                hit.collider.GetComponentInParent<ClickableInteractable>();

            if (interactable != null)
            {
                Debug.Log("Interacted with: " + interactable.name);
                interactable.OnInteract(hit);
            }
        }
    }
}