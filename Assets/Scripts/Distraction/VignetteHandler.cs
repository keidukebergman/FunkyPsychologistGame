using UnityEngine;

public class VignetteHandler : MonoBehaviour
{
    [SerializeField] private FatigueManager fatigueManager;
    [SerializeField] private Material vignetteMaterial;
    [SerializeField] private float max_curvature, min_curvature;
    [SerializeField] private float max_separation, min_separation;
    [SerializeField] private float min_fade, max_fade;
    public static VignetteHandler instance;

    private void Start()
    {
        instance = this;
        vignetteMaterial.SetFloat("_Active", 1);
    }

    void Update()
    {
        float displayFatigue = fatigueManager.GetDisplayFatigue();
        vignetteMaterial.SetFloat("_Curvature", Mathf.Lerp(min_curvature, max_curvature, displayFatigue));
        vignetteMaterial.SetFloat("_Separation", Mathf.Lerp(min_separation, max_separation, 3.333333333333f * (-displayFatigue + 0.3f)));
        vignetteMaterial.SetFloat("_FadeAmount", Mathf.Lerp(min_fade, max_fade, 1.43f * (displayFatigue - 0.3f)));
    }
    private void OnApplicationQuit()
    {
        vignetteMaterial.SetFloat("_Active", 0);
    }
}
