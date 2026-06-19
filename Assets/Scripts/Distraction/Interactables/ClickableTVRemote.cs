using UnityEngine;

public class ClickableTVRemote : ClickableInteractable
{
    [SerializeField] private ProgramSwitch _TVSwitch;

    public override void OnInteract(RaycastHit raycastHit)
    {
        //base.OnInteract(raycastHit);
        _TVSwitch.TurnOnTv();
    }
}
