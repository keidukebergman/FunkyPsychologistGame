using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleNotepad : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RawImage img;
    private Texture2D tex;

    void Start()
    {
        img = GetComponent<RawImage>();

        tex = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;

        for (int x = 0; x < 512; x++)
            for (int y = 0; y < 512; y++)
                tex.SetPixel(x, y, Color.white);

        tex.Apply();

        img.texture = tex;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Draw(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Draw(eventData);
    }

    void Draw(PointerEventData eventData)
    {
        RectTransform rt = img.rectTransform;

        Camera cam = eventData.pressEventCamera;
        if (cam == null)
            cam = Camera.main;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rt,
            eventData.position,
            cam,
            out Vector2 local))
            return;

        Rect r = rt.rect;

        int x = Mathf.FloorToInt(((local.x - r.xMin) / r.width) * 512);
        int y = Mathf.FloorToInt(((local.y - r.yMin) / r.height) * 512);

        x = Mathf.Clamp(x, 0, 511);
        y = Mathf.Clamp(y, 0, 511);

        tex.SetPixel(x, y, Color.black);
        tex.Apply();
    }
}