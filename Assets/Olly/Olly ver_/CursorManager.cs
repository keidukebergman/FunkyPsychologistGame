using UnityEngine;
using UnityEngine.UI;

public class CursorManager: MonoBehaviour
{
    [Header("Cursor Image")]
    [SerializeField] private Image cursorImage;

    [Header("Cursor Sprites")]
    [SerializeField] private Sprite defaultCursor;
    [SerializeField] private Sprite drawCursor;
    [SerializeField] private Sprite handCursor;
    [SerializeField] private Sprite inspectCursor;
    [SerializeField] private Sprite talkCursor;
    [SerializeField] private Sprite phoneCursor;
    [SerializeField] private Sprite tvCursor;
    [SerializeField] private Sprite buttonCursor;

    [Header("Raycast")]
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask rayMask = ~0;

    private Camera cam;

    void Start()
    {
        Debug.Log(cursorImage);
        Debug.Log(cursorImage.sprite);

            cam = GetComponent<Camera>();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;

            cursorImage.sprite = defaultCursor;

            Debug.Log("Cursor initialized");
    }

    void Update()
    {
        Debug.Log("CursorManager running");
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, rayMask))
        {
            CursorTarget target = hit.collider.GetComponent<CursorTarget>();

            if (target != null)
            {
                switch (target.cursorType)
                {
                    case CursorType.Draw:
                        SetCursor(drawCursor);
                        break;

                    case CursorType.Hand:
                        SetCursor(handCursor);
                        break;

                    case CursorType.Inspect:
                        SetCursor(inspectCursor);
                        break;

                    case CursorType.Talk:
                        SetCursor(talkCursor);
                        break;

                    case CursorType.Phone:
                        SetCursor(phoneCursor);
                        break;

                    case CursorType.TV:
                        SetCursor(tvCursor);
                        break;

                    case CursorType.Button:
                        SetCursor(buttonCursor);
                        break;

                    default:
                        SetCursor(defaultCursor);
                        break;
                }
            }
            else
            {
                SetCursor(defaultCursor);
            }
        }
        else
        {
            SetCursor(defaultCursor);
        }
    }

    private void SetCursor(Sprite sprite)
    {
        if (cursorImage.sprite != sprite)
            cursorImage.sprite = sprite;
    }
}