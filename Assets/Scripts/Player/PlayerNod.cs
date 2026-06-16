using System;
using UnityEngine;

public class PlayerNod : MonoBehaviour, Interactable
{
    public float nodCooldown = .2f;
    public float stimulationAmount = .2f;

    private float nodTime;

    private void Awake()
    {
        nodTime = 0;
    }

    public bool Nod()
    {
        if (Time.time >= nodTime)
        {

            OnInteract();
            nodTime = Time.time + nodCooldown;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnInteract()
    {
        SFXManager.PlayMhmmSFX();
        DistractionManager.instance.OnInteractWithPersistentInteractable(this, stimulationAmount * Time.deltaTime);
    }
}
