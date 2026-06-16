using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.UI.VirtualMouseInput;

public class Follow : MonoBehaviour
{
    public float mouseSensitivity = 15f;

    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -3f;
    public float maxY = 3f;
    public float smoothSpeed = 10f;

    private Vector3 targetPosition;
    private float initialZ;
    public Texture2D cursorTexture;
    public UnityEngine.CursorMode cursorMode = UnityEngine.CursorMode.Auto;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
        // Lock and hide the cursor so it doesn't leave the game window
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        // Save the starting positions
        initialZ = transform.position.z;
        targetPosition = transform.position;
    }

    void Update()
    {
        // 1. Get raw mouse movement input (delta)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 2. Add the movement to our target position
        targetPosition.x += mouseX;
        targetPosition.y += mouseY;

        // 3. Clamp the target position so it stays inside your Canvas limits
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        targetPosition.z = initialZ; // Keep the camera at its fixed 2D depth

        // 4. Smoothly slide the camera toward the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
