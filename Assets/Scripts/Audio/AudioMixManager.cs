using UnityEngine;
using UnityEngine.Audio;

public class AudioMixManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [Header("Pitch")]
    [SerializeField] private float pitchMin = 1;
    [SerializeField] private float pitchMax = 1;
    [Header("Lowpass")]
    [SerializeField] private float lowPassResonanceMin = 1;
    [SerializeField] private float lowPassResonanceMax = 1;
    [SerializeField] private float lowPassFrequencyMin = 9000;
    [SerializeField] private float lowPassFrequencyMax = 9000;
    [Header("Distortion")]
    [SerializeField] private float distortionLevelMin = 0;
    [SerializeField] private float distortionLevelMax = 0;
    [Header("Echo")]
    [SerializeField] private float dryMixMin = 0;
    [SerializeField] private float dryMixMax = 1;
    [SerializeField] private float wetMixMin = 0;
    [SerializeField] private float wetMixMax = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fatigueVal = 4*(FatigueManager.instance.GetFatigue() - 0.75f);
        SetValue("Pitch", pitchMin, pitchMax, fatigueVal);
        SetValue("LowPassCutoff", lowPassFrequencyMin, lowPassFrequencyMax, fatigueVal);
    }

    void SetValue(string name, float min, float max, float t)
    {
        float value = Mathf.Lerp(min, max, t);
        audioMixer.SetFloat(name, value);
    }
}
