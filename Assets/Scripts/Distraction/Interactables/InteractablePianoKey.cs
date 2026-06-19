using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class InteractablePianoKey : MonoBehaviour, Interactable
{
    private bool m_IsOneShot = true;
    public bool isOneShot { get => m_IsOneShot; set => m_IsOneShot = value; }

    public void OnInteract(RaycastHit raycastHit)
    {
        GetComponent<AudioSource>().Play();
        transform.parent.GetComponent<InteractablePiano>().OnInteract(raycastHit);
    }

}
