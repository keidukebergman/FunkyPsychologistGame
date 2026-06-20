using UnityEngine;

public class InteractableBobblehead : MonoBehaviour, Interactable
{

    private bool _oneShota = true;
    public bool isOneShot { get => _oneShota; set => _oneShota = value; }
    [SerializeField] private float stimulationValue = 0.1f;
    public void OnInteract(RaycastHit raycastHit)
    {
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, stimulationValue);
    }

}
