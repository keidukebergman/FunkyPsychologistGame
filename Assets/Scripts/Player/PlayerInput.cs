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
    public bool Key0Down { get; set; }
    public bool Key1Down { get; set; }
    public bool Key2Down { get; set; }
    public bool Key3Down { get; set; }
    public bool Key4Down { get; set; }
    public bool Key5Down { get; set; }
    public bool Key6Down { get; set; }
    public bool Key7Down { get; set; }
    public bool Key8Down { get; set; }
    public bool Key9Down { get; set; }

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

        Key0Down = Keyboard.current.numpad0Key.wasPressedThisFrame || Keyboard.current.digit0Key.wasPressedThisFrame;
        Key1Down = Keyboard.current.numpad1Key.wasPressedThisFrame || Keyboard.current.digit1Key.wasPressedThisFrame;
        Key2Down = Keyboard.current.numpad2Key.wasPressedThisFrame || Keyboard.current.digit2Key.wasPressedThisFrame;
        Key3Down = Keyboard.current.numpad3Key.wasPressedThisFrame || Keyboard.current.digit3Key.wasPressedThisFrame;
        Key4Down = Keyboard.current.numpad4Key.wasPressedThisFrame || Keyboard.current.digit4Key.wasPressedThisFrame;
        Key5Down = Keyboard.current.numpad5Key.wasPressedThisFrame || Keyboard.current.digit5Key.wasPressedThisFrame;
        Key6Down = Keyboard.current.numpad6Key.wasPressedThisFrame || Keyboard.current.digit6Key.wasPressedThisFrame;
        Key7Down = Keyboard.current.numpad7Key.wasPressedThisFrame || Keyboard.current.digit7Key.wasPressedThisFrame;
        Key8Down = Keyboard.current.numpad8Key.wasPressedThisFrame || Keyboard.current.digit8Key.wasPressedThisFrame;
        Key9Down = Keyboard.current.numpad9Key.wasPressedThisFrame || Keyboard.current.digit9Key.wasPressedThisFrame;
    }

    private void ResetInput()
    {
        MouseLeftDown = false;
        MouseRightDown = false;
        MouseDelta = 0;
    }
}
