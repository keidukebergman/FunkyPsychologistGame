using UnityEngine;

public class CameraInteractionRaycast : MonoBehaviour
{
    bool holding = false;
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            holding = true;
            Click();
        }
        
        if (Input.GetButtonUp("Interact"))
        {
            holding = false;
        }

        if (holding)
        {
            Debug.Log("Raycast firing");
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Interactable interactable = hit.transform.gameObject.GetComponent<Interactable>();
                if (interactable != null && !interactable.isOneShot)
                {
                    interactable.OnInteract(hit);
                }
                else
                {
                    Debug.Log("Raycast hit " + hit.collider.name);
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything. This should be impossible. You should look into this.");
            }
        }
    }

    private void Click()
    {
        Debug.Log("Raycast firing");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Interactable interactable = hit.transform.gameObject.GetComponent<Interactable>();
            if (interactable != null && interactable.isOneShot)
            {
                interactable.OnInteract(hit);
            }
            else
            {
                Debug.Log("Raycast hit " + hit.collider.name);
            }
        }
    }
}
