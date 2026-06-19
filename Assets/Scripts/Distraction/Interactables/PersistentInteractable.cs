using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentInteractable : MonoBehaviour, Interactable, IPointerDownHandler, IPointerUpHandler
{
    private bool pressed = false;
    [SerializeField] private float stimulationAmount = 0.5f;
    
    private bool isOneShot = false;
    bool Interactable.isOneShot { get => isOneShot; set => isOneShot = value; }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            OnInteract(new RaycastHit());
        }
    }

    public void OnInteract(RaycastHit raycastHit)
    {
        DistractionManager.instance.OnInteractWithPersistentInteractable(this, stimulationAmount * Time.deltaTime);
    }
}
