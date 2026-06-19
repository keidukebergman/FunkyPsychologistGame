using System.Collections.Generic;
using UnityEngine;
using UnityImage = UnityEngine.UI.Image;

public class SingleGifPlayer : MonoBehaviour
{
    public enum RenderMode
    {
        UGUIImage,
        MaterialOverride
    }

    [Header("Setup")]
    public RenderMode renderMode;
    public UnityImage targetImage;
    public Renderer targetRenderer;

    [HideInInspector]
    public string gifPath;

    private List<Texture2D> textures;
    private float[] delays;
    private Sprite[] sprites;

    private float timer;
    private int index;

    void Start()
    {
        if (!string.IsNullOrEmpty(gifPath))
            LoadGif(gifPath);
    }

    public void PlayGif(string path)
    {
        LoadGif(path);
    }

    private void LoadGif(string path)
    {
        gifPath = path;

        if (GifDatabase.Instance == null)
        {
            Debug.LogError("GifDatabase missing in scene!");
            return;
        }

        if (!GifDatabase.Instance.TryGet(path, out textures, out delays, out sprites))
        {
            Debug.LogError("GIF not found in database: " + path);
            return;
        }

        timer = 0;
        index = 0;
    }

    void Update()
    {
        if (textures == null || textures.Count == 0)
            return;

        timer += Time.deltaTime;

        if (timer > delays[index])
        {
            timer = 0;
            index++;

            if (index >= delays.Length)
                index = 0;
        }

        if (renderMode == RenderMode.MaterialOverride)
        {
            targetRenderer.material.mainTexture = textures[index];
        }
        else
        {
            targetImage.sprite = sprites[index];
        }
    }
}

/*
void Start()
{
    LoadGif();
}

void LoadGif()
{
    if (GifCacheManager.Instance == null)
    {
        Debug.LogError("No GifCacheManager in scene!");
        return;
    }

    if (!GifCacheManager.Instance.TryGet(gifPath, out texture2DList, out frameDelay, out sprites))
    {
        Debug.Log("Gif not cached yet, loading now: " + gifPath);
        GifCacheManager.Instance.PreloadGif(gifPath);

        GifCacheManager.Instance.TryGet(gifPath, out texture2DList, out frameDelay, out sprites);
    }
}
*/