using UnityEngine;

public class GifPreloader : MonoBehaviour
{
    public string[] gifsToLoad = { "/whitenoise.gif", "/ent1.gif", "/ent2.gif", "/ent3.gif", "/ent4.gif", "/ent5.gif", "/ent6.gif", "/ent7.gif", "/newtons - cradle1.gif", "/newtons - cradle.gif", "/KaijuBadEnd.gif", "/KaijuGoodEnd.gif", "/black-screen.gif" };


    void Start()
    {
        foreach (var gif in gifsToLoad)
        {
            GifCacheManager.Instance.PreloadGif(gif);
        }
    }
}