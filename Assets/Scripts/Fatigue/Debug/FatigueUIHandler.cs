using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FatigueUIHandler : MonoBehaviour
{
    [SerializeField] private FatigueManager fatigueManager;
    [SerializeField] private TextMeshProUGUI textMesh;
    void Start()
    {
        
    }

    void Update()
    {
        textMesh.text = fatigueManager.GetFatigue().ToString();
    }
}
