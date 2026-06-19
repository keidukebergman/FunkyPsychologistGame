using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class InteractablePianoKey : MonoBehaviour, Interactable
{
    Vector3 size;
    private bool m_IsOneShot = true;
    public bool isOneShot { get => m_IsOneShot; set => m_IsOneShot = value; }

    public void OnInteract(RaycastHit raycastHit)
    {
        GetComponent<AudioSource>().Play();
        transform.parent.GetComponent<InteractablePiano>().OnInteract(raycastHit);
        Vector3 scaling = transform.localScale;
        scaling.y = 0.5f * size.y;
        transform.localScale = scaling;
    }
    void Start()
    {
        size = transform.localScale;
    }
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, size, Time.deltaTime * 100);
    }

}
