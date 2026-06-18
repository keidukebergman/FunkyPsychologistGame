using System;
using UnityEngine;

public class CallManager : MonoBehaviour
{
    [SerializeField] private float startingSatisfaction = .5f;
    [Space]
    [SerializeField] private AudioSource m_clientSource;
    [SerializeField] private AudioSource m_phonesource;
    [Space]
    [SerializeField] private AudioClip[] m_phoneSFX;

    public bool CallOver { get; set; }
    public bool CallIsOver => m_callClientSpeechIndex >= 1 && CallOver;

    public Action<int> OnSpeechFinished;
    public Client CurrentClient { get; private set; }
    
    private int m_callClientSpeechIndex = 0;
    private float _clientSatisfaction;
    public float ClientSatisfaction
    {
        get => _clientSatisfaction;
        private set
        {
            _clientSatisfaction = value;
            //Debug.Log("Client Satisfaction: " + _clientSatisfaction);
        }
    }

    public bool InDecision { get; private set; }

    private ChoiceManager _choiceManager;

    private void Awake()
    {
        _choiceManager = GetComponent<ChoiceManager>();

    }


    private float beforeSample = 1;
    public void Tick()
    {
        if (beforeSample < m_clientSource.timeSamples)
        {
            // Still Playing Audio, Keep Following Value!
            beforeSample = m_clientSource.timeSamples - 1f;
        }
        else if (beforeSample > m_clientSource.timeSamples)
        {
            // If 'beforeSample' bigger than timeSamples,
            // It means timeSamples set '0'.

            SpeechFinished();

            beforeSample = m_clientSource.timeSamples - 1f;
        }
    }

    public void PlayCall(Client p_client)
    {
        CallOver = false;
        InDecision = false;
        CurrentClient = p_client;

        ClientSatisfaction = startingSatisfaction;
        m_callClientSpeechIndex = 0;

        PlayClientSpeech();
    }

    private void SpeechFinished()
    {
        switch (m_callClientSpeechIndex)
        {
            case 0:
                InDecision = true;
                OnSpeechFinished?.Invoke(m_callClientSpeechIndex);
                break;
            case 1:
                InDecision = true;
                OnSpeechFinished?.Invoke(m_callClientSpeechIndex);
                break;
            default:
                break;
        }
    }

    public void ProceedCall()
    {
        InDecision = false;
        m_callClientSpeechIndex++;
        if (m_callClientSpeechIndex >= 2)
            CallFinished();
        else
            PlayClientSpeech();
    }

    private void CallFinished()
    {
        CallOver = true;
    }

    private void PlayClientSpeech()
    {
        m_clientSource.clip = CurrentClient.Speeches[m_callClientSpeechIndex].Clip;
        m_clientSource.Play();
    }

    public void StopCall()
    {
        m_clientSource.Stop();
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
