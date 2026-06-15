using UnityEngine;

public class VignetteHandler : MonoBehaviour
{
    [SerializeField] private FatigueManager fatigueManager;
    [SerializeField] private Material vignetteMaterial;
    [SerializeField] private float max_curvature;
    [SerializeField] private float min_curvature;

    // Update is called once per frame
    void Update()
    {
        vignetteMaterial.SetFloat("_Curvature", Mathf.Lerp(min_curvature, max_curvature, fatigueManager.GetFatigue()));
    }
}
