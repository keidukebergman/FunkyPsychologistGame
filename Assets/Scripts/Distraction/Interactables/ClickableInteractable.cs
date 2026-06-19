using UnityEngine;

public class ClickableInteractable : MonoBehaviour, Interactable
{
    [SerializeField] private float cooldown = 0.4f;
    [SerializeField] private float stimulationAmount = 0.3f;
    [Space]
    [SerializeField] private Material[] materials;
    int material_index = 0;

    private float timer = 0;

    private bool isOneShot = true;
    bool Interactable.isOneShot { get => isOneShot; set => isOneShot = value; }

    protected virtual void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public virtual void OnInteract(RaycastHit raycastHit)
    {
        if (timer > 0) return;
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, stimulationAmount);
        timer = cooldown;
        //material_index = (material_index + 1) % (materials.Length);
        //GetComponent<Renderer>().material = materials[material_index];
    }
}
