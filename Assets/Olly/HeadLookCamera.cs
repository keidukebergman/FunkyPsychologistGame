using UnityEngine;

public class HeadLookCamera : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float maxYaw = 15f;
    [SerializeField] private float maxPitch = 10f;
    [SerializeField] private float smoothSpeed = 5f;

    [Header("Edge Zones")]
    [SerializeField] private float edgeSize = 0.15f;
    // 0.15 = outer 15% of screen

    private Quaternion startRotation;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        startRotation = transform.localRotation;
    }

    void Update()
    {
        float x = Input.mousePosition.x / Screen.width;
        float y = Input.mousePosition.y / Screen.height;

        float yaw = 0f;
        float pitch = 0f;

        // LEFT EDGE
        if (x < edgeSize)
        {
            float t = 1f - (x / edgeSize);
            yaw = -maxYaw * t;
        }

        // RIGHT EDGE
        if (x > 1f - edgeSize)
        {
            float t = (x - (1f - edgeSize)) / edgeSize;
            yaw = maxYaw * t;
        }

        // TOP EDGE
        if (y > 1f - edgeSize)
        {
            float t = (y - (1f - edgeSize)) / edgeSize;
            pitch = -maxPitch * t;
        }

        // BOTTOM EDGE
        if (y < edgeSize)
        {
            float t = 1f - (y / edgeSize);
            pitch = maxPitch * t;
        }

        Quaternion targetRotation =
            startRotation *
            Quaternion.Euler(pitch, yaw, 0);

        transform.localRotation =
            Quaternion.Slerp(
                transform.localRotation,
                targetRotation,
                smoothSpeed * Time.deltaTime);
    }
}