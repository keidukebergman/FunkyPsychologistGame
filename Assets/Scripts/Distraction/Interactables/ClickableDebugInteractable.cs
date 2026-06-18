using UnityEngine;

public class ClickableDebugInteractable : MonoBehaviour, Interactable
{
    [SerializeField] private float cooldown = 0.4f;
    private float timer = 0;
    [SerializeField] private Material[] materials;
    int material_index = 0;
    public bool isOneShot = true;

    bool Interactable.isOneShot { get => isOneShot; set => isOneShot = value; }

    public void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void OnInteract(RaycastHit raycastHit)
    {
        if (timer > 0) return;
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, 0.3f);
        timer = cooldown;
        material_index = (material_index + 1) % (materials.Length);
        GetComponent<Renderer>().material = materials[material_index];
    }
}
