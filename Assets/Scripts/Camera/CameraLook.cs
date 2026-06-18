using UnityEngine;


public class CameraLook : MonoBehaviour
{
    [Header("Sensitivity")]
    [Tooltip("Horizontal (left/right) mouse sensitivity.")]
    [SerializeField] private float sensitivityX = 200f;

    [Tooltip("Vertical (up/down) mouse sensitivity.")]
    [SerializeField] private float sensitivityY = 200f;

    [Header("Vertical Look Limits")]
    [Tooltip("Maximum degrees the camera can pitch upward.")]
    [SerializeField] private float maxLookUpAngle = 80f;

    [Tooltip("Maximum degrees the camera can pitch downward.")]
    [SerializeField] private float maxLookDownAngle = 80f;

    [Header("Cursor")]
    [SerializeField] private bool lockCursor = true;

    private float currentPitch = 0f;
    private float currentYaw = 0f;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        currentPitch = transform.localEulerAngles.x;
        currentYaw = transform.localEulerAngles.y;
        if (currentYaw > 180f) currentYaw -= 360f;
        if (currentPitch > 180f) currentPitch -= 360f;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        currentPitch -= mouseY;
        currentYaw += mouseX;
        currentPitch = Mathf.Clamp(currentPitch, -maxLookUpAngle, maxLookDownAngle);
        transform.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }

    public void SetCursorLock(bool locked)
    {
        lockCursor = locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}
