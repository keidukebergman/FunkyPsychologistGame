using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.IO;

public class GifCacheManager : MonoBehaviour
{
    public static GifCacheManager Instance;

    private Dictionary<string, CachedGif> cache = new Dictionary<string, CachedGif>();

    private class CachedGif
    {
        public List<Texture2D> frames;
        public float[] delays;
        public Sprite[] sprites;
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PreloadGif(string path)
    {
        if (cache.ContainsKey(path))
            return;

        string fullPath = Application.streamingAssetsPath + path;

        if (!File.Exists(fullPath))
        {
            Debug.LogError("GIF not found: " + fullPath);
            return;
        }

        Image image = Image.FromFile(fullPath);

        GifPlayerBase.Gif2Texture2DWithFrameDelay(
            image,
            out List<Texture2D> frames,
            out float[] delays
        );

        Sprite[] sprites = new Sprite[frames.Count];

        for (int i = 0; i < frames.Count; i++)
        {
            sprites[i] = Sprite.Create(
                frames[i],
                new Rect(0, 0, frames[i].width, frames[i].height),
                new Vector2(0.5f, 0.5f)
            );
        }

        cache[path] = new CachedGif
        {
            frames = frames,
            delays = delays,
            sprites = sprites
        };
    }

    public bool TryGet(string path, out List<Texture2D> frames, out float[] delays, out Sprite[] sprites)
    {
        if (cache.TryGetValue(path, out CachedGif gif))
        {
            frames = gif.frames;
            delays = gif.delays;
            sprites = gif.sprites;
            return true;
        }

        frames = null;
        delays = null;
        sprites = null;
        return false;
    }
}