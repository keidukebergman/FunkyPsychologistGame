using UnityEngine;

public class InteractablePiano : MonoBehaviour, Interactable
{
    [SerializeField] private float stimulation;
    public bool isOneShot = true;
    bool Interactable.isOneShot { get => isOneShot; set => isOneShot = value; }
    public void Start()
    {
        int children = transform.childCount;
        for (int i = 1; i < children; i++)
        {
            AudioSource audioSource = transform.GetChild(i).GetComponent<AudioSource>();
            audioSource.pitch = 1 * Mathf.Pow(1.05946f, i - 1);
        }
    }
    public void OnInteract(RaycastHit raycastHit)
    {
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, stimulation);
    }
}
