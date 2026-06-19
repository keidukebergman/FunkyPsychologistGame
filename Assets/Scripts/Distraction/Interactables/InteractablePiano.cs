using UnityEngine;

public class InteractablePiano : MonoBehaviour, Interactable
{
    [SerializeField] private float stimulation;
    public bool isOneShot = true;
    bool Interactable.isOneShot { get => isOneShot; set => isOneShot = value; }
    public void OnInteract(RaycastHit raycastHit)
    {
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, stimulation);
    }
}
