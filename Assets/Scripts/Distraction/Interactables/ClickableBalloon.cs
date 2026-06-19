using UnityEngine;

public class ClickableBalloon : ClickableInteractable
{
    [SerializeField] private SFX m_sfxPop;
    [SerializeField] private GameObject m_particles;
    [Space]
    [Tooltip("How strongly the balloon floats upward. Must be higher than the gravity force.")]
    public float floatStrength = 15;
    [Tooltip("Adds a tiny bit of random horizontal drifting to look natural.")]
    public float driftStrength = 0.5f;

    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;

    public MeshRenderer MeshRenderer => _meshRenderer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void FixedUpdate()
    {
        // 1. Calculate the upward buoyancy force vector
        Vector3 buoyancyForce = Vector3.up * floatStrength;

        // 2. Generate a gentle, organic sway over time using Mathf.Sin
        float horizontalDriftX = Mathf.Sin(Time.time * 2f) * driftStrength;
        float horizontalDriftZ = Mathf.Cos(Time.time * 1.5f) * driftStrength;
        Vector3 driftForce = new Vector3(horizontalDriftX, 0f, horizontalDriftZ);

        // 3. Apply the combined forces to the physics engine
        _rb.AddForce(buoyancyForce + driftForce, ForceMode.Force);
    }
    private void SpawnVFX()
    {
        Instantiate(m_particles, transform.position, Quaternion.identity);
    }

    public override void OnInteract(RaycastHit raycastHit)
    {
        base.OnInteract(raycastHit);
        OnPop();
    }

    public void OnPop()
    {
        SpawnVFX();
        SFXManager.PlaySFX(m_sfxPop.Clip, m_sfxPop.Volume);
        gameObject.SetActive(false);
    }
}
