using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using DrawingImage = System.Drawing.Image;

public class GifDatabase : MonoBehaviour
{
    public static GifDatabase Instance;

    [Header("All GIF paths (relative to StreamingAssets)")]
    public string[] gifPaths = { "/whitenoise.gif", "/ent1.gif", "/ent2.gif", "/ent3.gif", "/ent4.gif", "/ent5.gif", "/ent6.gif", "/ent7.gif" };

    private class GifData
    {
        public List<Texture2D> textures;
        public float[] delays;
        public Sprite[] sprites;
    }

    private Dictionary<string, GifData> cache = new Dictionary<string, GifData>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        PreloadAllGIFs();
    }

    private void PreloadAllGIFs()
    {
        foreach (var path in gifPaths)
        {
            if (cache.ContainsKey(path))
                continue;

            Debug.Log("Preloading GIF: " + path);

            try
            {
                DrawingImage image =
                    DrawingImage.FromFile(Application.streamingAssetsPath + path);

                List<Texture2D> textures;
                float[] delays;

                GifPlayerBase.Gif2Texture2DWithFrameDelay(
                    image,
                    out textures,
                    out delays);

                Sprite[] sprites = new Sprite[textures.Count];

                for (int i = 0; i < textures.Count; i++)
                {
                    sprites[i] = Sprite.Create(
                        textures[i],
                        new Rect(0, 0, textures[i].width, textures[i].height),
                        new Vector2(0.5f, 0.5f));
                }

                cache[path] = new GifData
                {
                    textures = textures,
                    delays = delays,
                    sprites = sprites
                };
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load GIF: " + path + "\n" + e);
            }
        }

        Debug.Log("GIF preload complete!");
    }

    public bool TryGet(string path, out List<Texture2D> textures, out float[] delays, out Sprite[] sprites)
    {
        textures = null;
        delays = null;
        sprites = null;

        if (!cache.TryGetValue(path, out var data))
            return false;

        textures = data.textures;
        delays = data.delays;
        sprites = data.sprites;
        return true;
    }
}