using UnityEngine;

public class ClickableInteractable : MonoBehaviour, Interactable
{
    [SerializeField] private float cooldown = 0.4f;
    [SerializeField] private float fatigueRestoration = 0.3f;

    private float timer = 0;

    protected virtual void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public virtual void OnInteract()
    {
        if (timer > 0) return;
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, fatigueRestoration);
        timer = cooldown;
    }
}
