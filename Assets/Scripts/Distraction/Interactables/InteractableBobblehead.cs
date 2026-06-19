using UnityEngine;

public class InteractableBobblehead : MonoBehaviour, Interactable
{
    public bool isOneShot { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    [SerializeField] private float stimulationValue = 0.1f;
    public void OnInteract(RaycastHit raycastHit)
    {
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, stimulationValue);
    }

}
