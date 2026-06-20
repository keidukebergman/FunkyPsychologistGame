using System;
using UnityEngine;

public class ClickableTelephone : ClickableInteractable
{
    public Action OnClick;

    public bool Held { get; set; }

    public override void OnInteract(RaycastHit raycastHit)
    {
        // should telephone reduce fatigue while picking it up once?

        //base.OnInteract(raycastHit);
        Debug.Log("Telephone OnInteract");

        if (OnClick == null)
            Debug.Log("OnClick is NULL!");

        OnClick?.Invoke();
    }
}
