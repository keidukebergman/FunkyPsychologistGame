using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class InteractablePianoKey : MonoBehaviour, Interactable
{
    Vector3 size;
    private bool m_IsOneShot = true;
    public bool isOneShot { get => m_IsOneShot; set => m_IsOneShot = value; }

    private Vector3 _pos;
    public float _offsetAmount = 0.2f;

    public void OnInteract(RaycastHit raycastHit)
    {
        GetComponent<AudioSource>().Play();
        transform.parent.GetComponent<InteractablePiano>().OnInteract(raycastHit);
        Vector3 scaling = transform.position;
        scaling.y -= _offsetAmount;
        transform.position = scaling;
    }
    void Start()
    {
        size = transform.localScale;
        _pos = transform.position;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _pos, Time.deltaTime * 1);
    }

}
