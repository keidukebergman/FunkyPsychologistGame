using UnityEngine;

public class CursorTarget : MonoBehaviour
{
    public CursorType cursorType = CursorType.Default;
}

public enum CursorType
{
    Default,
    Draw,
    Hand,
    Inspect,
    Talk,
    Phone,
    TV,
    Button
}