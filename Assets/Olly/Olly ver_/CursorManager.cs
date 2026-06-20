using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Image cursorImage;

    [Header("Cursor Sprites")]
    [SerializeField] private Sprite defaultCursor;
    [SerializeField] private Sprite interactCursor;
    [SerializeField] private Sprite drawCursor;
    [SerializeField] private Sprite eyeCursor;

    [Header("Scale")]
    //Cursor shrinks to only 20% of its normal size when far away
    [SerializeField] private float minScale = 0.5f;
    //Cursor becomes 150% of its normal size when you're close
    //For the cursor to become noticeably smaller, lower this value
    [SerializeField] private float maxScale = 1.6f;
    //determines how quickly the cursor reaches minScale
    [SerializeField] private float maxDistance = 2.5f;
    [SerializeField] private float scaleSmooth = 10f;

    [Header("Stick To Surface")]

    [SerializeField] private bool stickToSurface = true;
    [SerializeField] private float stickSpeed = 15f;


    [Header("Cursor Offsets")]
    [SerializeField] private Vector2 defaultOffset;
    [SerializeField] private Vector2 interactOffset;
    [SerializeField] private Vector2 drawOffset;
    [SerializeField] private Vector2 drawtableOffset; 
    [SerializeField] private Vector2 eyeOffset;

    [SerializeField] private RectTransform tableRect;

    [Header("Drawing Cursor Offsets")]
    [SerializeField] private Vector2 bottomOffset = new Vector2(-18, 8);
    [SerializeField] private Vector2 topOffset = new Vector2(-35, 20);
    [SerializeField] private Vector2 topLeftOffset;
    [SerializeField] private Vector2 topRightOffset;
    [SerializeField] private Vector2 bottomLeftOffset;
    [SerializeField] private Vector2 bottomRightOffset;

    private float currentRotation;
    //set Cursor Size in the Inspector to values like:
    //0.5 = smaller
    //1 = normal
    //2 = twice as large
    //3 = huge
    [SerializeField] private float cursorSize = 0.5f;


    [SerializeField] private bool lockWhileDrawing = true;

    private bool drawing;
    private Vector3 lockedPosition;
    private Vector2 currentOffset;
    private RectTransform cursorRect;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        cursorRect = cursorImage.rectTransform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        cursorImage.sprite = defaultCursor;
    }

    void Update()
    {

        if (drawing)
        {
            // Smoothly follow the real mouse while staying "heavy"
            lockedPosition = Vector3.Lerp(
                lockedPosition,
                Input.mousePosition,
                Time.deltaTime * 2f);

            cursorRect.position = lockedPosition + (Vector3)currentOffset;

            if (!Input.GetMouseButton(0))
                drawing = false;

            return;
        }
        Vector3 targetPosition = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.collider.name + " Tag: " + hit.collider.tag);
            Vector3 localHit = hit.collider.transform.InverseTransformPoint(hit.point);
            // ---------- Cursor sprite ----------
            if (hit.collider.CompareTag("Drawable"))
            {
                cursorImage.sprite = drawCursor;
                currentOffset = drawOffset;


            }
            if (hit.collider.CompareTag("Drawable"))
            {
                cursorImage.sprite = drawCursor;

                if (lockWhileDrawing && Input.GetMouseButtonDown(0))
                {
                    drawing = true;
                    lockedPosition = Input.mousePosition;
                }
            }
            else if (hit.collider.CompareTag("Interactable"))
            {
                cursorImage.sprite = interactCursor;
                currentOffset = drawOffset;

            }
            else if (hit.collider.CompareTag("Eyeable"))
            {
                cursorImage.sprite = eyeCursor;
                currentOffset = eyeOffset;

            }
            else if (hit.collider.CompareTag("TableDraw"))
            {
                cursorImage.sprite = drawCursor;

                Vector2 localPoint;

                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    tableRect,
                    Input.mousePosition,
                    cam,
                    out localPoint))
                {
                    float tx = Mathf.InverseLerp(
                        -tableRect.rect.width * 0.5f,
                         tableRect.rect.width * 0.5f,
                         localPoint.x);

                    float ty = Mathf.InverseLerp(
                        -tableRect.rect.height * 0.5f,
                         tableRect.rect.height * 0.5f,
                         localPoint.y);

                    Vector2 bottom = Vector2.Lerp(bottomLeftOffset, bottomRightOffset, tx);
                    Vector2 top = Vector2.Lerp(topLeftOffset, topRightOffset, tx);

                    currentOffset = Vector2.Lerp(bottom, top, ty);

                    Debug.Log($"Local: {localPoint}  Offset: {currentOffset}");

                    currentOffset = new Vector2(-500, 0);
                }
                else
                {
                    currentOffset = bottomOffset;
                }

                if (lockWhileDrawing && Input.GetMouseButtonDown(0))
                {
                    drawing = true;
                    lockedPosition = Input.mousePosition;
                }
                else
                {
                    currentOffset = bottomOffset;
                }
            }
            
        }
        else
        {
            cursorImage.sprite = defaultCursor;
            currentOffset = drawOffset;
                Debug.Log("Nothing hit");
        }

        // Cursor scale
        float t = Mathf.Clamp01(hit.distance / maxDistance);
        float targetScale = Mathf.Lerp(maxScale, minScale, t);

        cursorRect.localScale = Vector3.Lerp(
                cursorRect.localScale,
                Vector3.one * targetScale * cursorSize,
                Time.deltaTime * scaleSmooth);


        // Stick cursor to hit point
 /*if (stickToSurface)
    {
       targetPosition = cam.WorldToScreenPoint(hit.point);
       }
        

        else
        {
            cursorImage.sprite = defaultCursor;

            cursorRect.localScale = Vector3.Lerp(
                cursorRect.localScale,
                Vector3.one * cursorSize,
                Time.deltaTime * scaleSmooth);
        }
 */
targetPosition = Input.mousePosition;

cursorRect.position = Vector3.Lerp(cursorRect.position, targetPosition + (Vector3) currentOffset,
        Time.deltaTime* stickSpeed);

    }
    
}