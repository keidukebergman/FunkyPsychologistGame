using UnityEngine;

public class InteractableFidgetSpinner : MonoBehaviour, Interactable
{
    public bool isOneShot { get => true; set => throw new System.NotImplementedException(); }
    [SerializeField] private float m_focusValue = 0.1f;
    [SerializeField] private GameObject explosion;

    public void OnInteract(RaycastHit raycastHit)
    {
        Camera cam = Camera.main;
        Vector3 dir = raycastHit.point - cam.transform.position;
        GetComponent<Rigidbody>().AddForceAtPosition(1f * dir.normalized, raycastHit.point, ForceMode.Impulse);
        DistractionManager.instance.OnInteractWithSingleUseInteractable(this, m_focusValue);
        print(GetComponent<Rigidbody>().angularVelocity.y);
        if (Mathf.Abs(GetComponent<Rigidbody>().angularVelocity.y) > 49)
        {
            explosion.SetActive(true);
            Destroy(gameObject);
        }
    }
}
