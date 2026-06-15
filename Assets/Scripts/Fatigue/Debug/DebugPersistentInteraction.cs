using UnityEngine;
using UnityEngine.EventSystems;

public class DebugPersistentInteraction : MonoBehaviour, Interactable, IPointerDownHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool pressed = false;
    [SerializeField] private float stimulationAmount = 0.5f;
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
            OnInteract();
        }
    }

    public void OnInteract()
    {
        DistractionManager.instance.OnInteractWithPersistentInteractable(this, stimulationAmount * Time.deltaTime);
    }
}
