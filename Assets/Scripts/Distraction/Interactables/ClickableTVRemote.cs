using UnityEngine;

public class ClickableTVRemote : ClickableInteractable
{
    [SerializeField] private ProgramSwitch _TVSwitch;

    public override void OnInteract()
    {
        //base.OnInteract();
        _TVSwitch.TurnOnTv();
    }
}
