using UnityEngine;

public class VignetteHandler : MonoBehaviour
{
    [SerializeField] private FatigueManager fatigueManager;
    [SerializeField] private Material vignetteMaterial;
    [SerializeField] private float max_curvature, min_curvature;
    [SerializeField] private float max_separation, min_separation;

    private void Start()
    {
        vignetteMaterial.SetFloat("_Active", 1);
    }
    void Update()
    {
        vignetteMaterial.SetFloat("_Curvature", Mathf.Lerp(min_curvature, max_curvature, (fatigueManager.GetFatigue())));
        vignetteMaterial.SetFloat("_Separation", Mathf.Lerp(min_separation, max_separation, 3.333333333333f*(-fatigueManager.GetFatigue() + 0.3f)));
    }
    private void OnApplicationQuit()
    {
        vignetteMaterial.SetFloat("_Active", 0);
    }
}
