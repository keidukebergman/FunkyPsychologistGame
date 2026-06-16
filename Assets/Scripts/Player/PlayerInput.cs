using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    private bool m_lastMouseLeftValue;
    private bool m_lastMouseRightValue;

    private bool m_mouseLeftHeld;
    private bool m_mouseRightHeld;

    public bool MouseLeftDown { get; set; }
    public bool MouseLeftHeld { get; set; }
    public bool MouseLeftUp { get; set; }

    public bool MouseRightDown { get; set; }
    public bool MouseRightHeld { get; set; }
    public bool MouseRightUp { get; set; }

    public bool ScrollWheelDown { get; set; }
    public Vector2 ScrollWheelDelta { get; set; }
    public bool ScrollWheelUp { get; set; }

    public bool SpacebarDown { get; set; }

    public float MouseDelta { get; set; }

    private bool m_lock;
    public bool MouseLock
    {
        get => m_lock; set
        {
            m_lock = value;
            ResetInput();
        }
    }

    public void Tick()
    {
        if (MouseLock)
        {
            return;
        }


        MouseLeftDown = Mouse.current.leftButton.wasPressedThisFrame;
        MouseRightDown = Mouse.current.rightButton.wasPressedThisFrame;
        ScrollWheelDown = Mouse.current.middleButton.wasPressedThisFrame;

        MouseLeftHeld = Mouse.current.leftButton.isPressed;
        MouseRightHeld = Mouse.current.rightButton.isPressed;

        MouseLeftUp = Mouse.current.leftButton.wasReleasedThisFrame;
        MouseRightUp = Mouse.current.rightButton.wasReleasedThisFrame;
        ScrollWheelUp = Mouse.current.middleButton.wasReleasedThisFrame;

        SpacebarDown = Keyboard.current.spaceKey.wasPressedThisFrame;
    }

    private void ResetInput()
    {
        MouseLeftDown = false;
        MouseRightDown = false;
        MouseDelta = 0;
    }
}
