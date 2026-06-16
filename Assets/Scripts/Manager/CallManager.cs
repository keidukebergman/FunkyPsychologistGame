using UnityEngine;

public class CallManager : MonoBehaviour
{
    [SerializeField] private float startingSatisfaction = .5f;
    [Space]
    [SerializeField] private AudioSource m_source;
    [SerializeField] private AudioSource m_phonesource;
    [Space]
    [SerializeField] private AudioClip[] m_phoneSFX;

    public bool CallOver { get; set; }

    public Client CurrentClient { get; private set; }

    private float _clientSatisfaction;
    public float ClientSatisfaction
    {
        get => _clientSatisfaction;
        private set
        {
            _clientSatisfaction = value;
            Debug.Log("Client Satisfaction: " + _clientSatisfaction);
        }
    }

    public void FixedUpdate()
    {
        if (!CallOver)
        {
            CallOver = !m_source.isPlaying;
        }
    }

    public void PlayCall(Client p_client)
    {
        CallOver = false;
        CurrentClient = p_client;

        ClientSatisfaction = startingSatisfaction;

        m_source.clip = CurrentClient.Speeches[0].Clip;

        m_source.Play();
    }

    public void OnSatisfied(float p_percent)
    {
        ClientSatisfaction += p_percent;
    }

    public void OnDispleased(float p_percent)
    {
        ClientSatisfaction -= p_percent;
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
