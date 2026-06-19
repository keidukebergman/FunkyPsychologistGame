using UnityEngine;

public interface Interactable
{
    public bool isOneShot {  get; set; }
    public void OnInteract(RaycastHit raycastHit);
}
