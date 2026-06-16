using UnityEngine;

public class CallManager : MonoBehaviour
{

    [SerializeField] private AudioSource m_source;
    [SerializeField] private AudioSource m_phonesource;
    [Space]
    [SerializeField] private AudioClip[] m_calls;
    [SerializeField] private AudioClip[] m_phoneSFX;

    public bool CallOver { get; set; }

    private void Awake()
    {

    }

    public void PlayCall(Client p_client)
    {
        CallOver = false;

        m_source.clip = p_client.Speeches[0].Clip; //m_calls[Random.Range(0, m_calls.Length)];

        m_source.Play();
    }

    public void FixedUpdate()
    {
        if (!CallOver)
        {
            CallOver = !m_source.isPlaying;
        }
    }

    public void PlayRinging()
    {
        m_phonesource.clip = m_phoneSFX[0];
        m_phonesource.Play();
    }

    public void StopRinging()
    {
        m_phonesource.Stop();
    }

    public void PickUpSFX()
    {
        m_phonesource.Stop();
        m_phonesource.clip = m_phoneSFX[1];
        m_phonesource.Play();
    }

    public void PlaceSFX()
    {
        m_phonesource.Stop();
        m_phonesource.clip = m_phoneSFX[2];
        m_phonesource.Play();
    }
}
