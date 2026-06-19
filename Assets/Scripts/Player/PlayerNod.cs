using System;
using UnityEngine;

public class PlayerNod : MonoBehaviour, Interactable
{
    public float nodCooldown = .2f;
    public float stimulationAmount = 5f;

    private float nodTime;
    public bool isOneShot = true;

    bool Interactable.isOneShot { get => isOneShot; set => isOneShot = value; }

    private void Awake()
    {
        nodTime = 0;
    }

    public bool Nod()
    {
        if (Time.time >= nodTime)
        {

            OnInteract(new RaycastHit());
            nodTime = Time.time + nodCooldown;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnInteract(RaycastHit raycastHit)
    {
        SFXManager.PlayMhmmSFX();
        DistractionManager.instance.OnInteractWithPersistentInteractable(this, stimulationAmount * Time.deltaTime);
    }
}
