using UnityEngine;

public class InteractableBobbleComponent : MonoBehaviour, Interactable
{
    public bool isOneShot { get => true; set => throw new System.NotImplementedException(); }

    public void OnInteract(RaycastHit raycastHit)
    {
        Vector3 direction = raycastHit.point - Camera.main.transform.position;
        Vector3 impact = raycastHit.point;
        GetComponent<Rigidbody>().AddForceAtPosition(direction, impact*3, ForceMode.Impulse);
        transform.parent.GetComponent<InteractableBobblehead>().OnInteract(raycastHit);
    }
}
