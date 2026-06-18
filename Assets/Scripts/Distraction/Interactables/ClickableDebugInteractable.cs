using UnityEngine;

public class ClickableDebugInteractable : MonoBehaviour, Interactable
{
    [SerializeField] private float cooldown = 0.4f;
    private float timer = 0;

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
        GetComponent<Rigidbody>().AddForceAtPosition(-raycastHit.normal * 10, raycastHit.point, ForceMode.Impulse);
    }
}
