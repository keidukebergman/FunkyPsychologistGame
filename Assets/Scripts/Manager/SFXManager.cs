using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    private AudioSource Source;

    public static SFXManager Instance;

    [SerializeField] private SFXCollection m_sfxCollection;
    [Space]
    [SerializeField] private AudioMixerGroup m_sfxMixerGroup;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    static public void PlayMhmmSFX() => Instance.PlayMhmmSFX_Internal();
    private void PlayMhmmSFX_Internal() => PlaySFX_Internal(m_sfxCollection[0].Clip, m_sfxCollection[0].Volume);

    static public void PlaySFX(AudioClip p_clip, float p_volume = 1, bool p_randomizePitch = false)
    {
        Instance.PlaySFX_Internal(p_clip, p_volume, p_randomizePitch);
    }
    private void PlaySFX_Internal(AudioClip p_clip, float p_volume, bool p_randomizePitch = false)
    {
        if (p_clip == null)
            return;

        Source.PlayOneShot(p_clip, p_volume);
    }
}
