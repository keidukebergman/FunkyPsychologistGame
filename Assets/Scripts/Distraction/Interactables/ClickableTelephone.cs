using System;
using UnityEngine;

public class ClickableTelephone : ClickableInteractable
{
    public Action OnClick;

    public bool Held { get; set; }

    public override void OnInteract()
    {
        //base.OnInteract();
        OnClick?.Invoke();
    }
}
