using UnityEngine;

public class GifPreloader : MonoBehaviour
{
    public string[] gifsToLoad;

    void Start()
    {
        foreach (var gif in gifsToLoad)
        {
            GifCacheManager.Instance.PreloadGif(gif);
        }
    }
}