using System;
using UnityEngine;

public class DrawControllerForRaycast : MonoBehaviour, Interactable
{
    [SerializeField] private Color color = Color.red;
    [SerializeField] private int brushSize = 10;
    [SerializeField] private Vector2 textureSize = new Vector2(1920, 2714);

    private Texture2D drawTexture;
    private bool oneShot = false;

    public bool isOneShot { get => oneShot; set => oneShot = value; }

    private void Start()
    {
        InitializeTexture();
    }

    private void InitializeTexture()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) return;

        Texture2D original = renderer.material.mainTexture as Texture2D;
        if (original != null)
        {
            drawTexture = Instantiate(original);
        }
        else
        {
            drawTexture = new Texture2D(Mathf.RoundToInt(textureSize.x), Mathf.RoundToInt(textureSize.y), TextureFormat.RGBA32, false);
            Color[] pixels = new Color[Mathf.RoundToInt(textureSize.x) * Mathf.RoundToInt(textureSize.y)];
            for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.white;
            drawTexture.SetPixels(pixels);
            drawTexture.Apply();
        }

        renderer.material.mainTexture = drawTexture;
    }

    public void OnInteract(RaycastHit raycastHit)
    {
        if (drawTexture == null) return;

        Vector2 uv = raycastHit.textureCoord;

        int x = (int)(uv.x * drawTexture.width);
        int y = (int)(uv.y * drawTexture.height);

        Paint(x, y);
    }

    private void Paint(int centerX, int centerY)
    {
        int halfBrush = brushSize / 2;

        for (int x = centerX - halfBrush; x <= centerX + halfBrush; x++)
        {
            for (int y = centerY - halfBrush; y <= centerY + halfBrush; y++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                if (dist > halfBrush) continue;

                int clampedX = Mathf.Clamp(x, 0, drawTexture.width - 1);
                int clampedY = Mathf.Clamp(y, 0, drawTexture.height - 1);

                drawTexture.SetPixel(clampedX, clampedY, color);
            }
        }

        drawTexture.Apply();
    }
}