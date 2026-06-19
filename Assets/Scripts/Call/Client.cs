using UnityEngine;

[CreateAssetMenu(fileName = "Client", menuName = "Scriptables/Client/New Client", order = 0)]
public class Client : ScriptableObject
{
    [SerializeField] private string _clientName;
    [SerializeField] private float _satisfactionThreshold = .8f;
    [Space]
    [SerializeField] private int _cashQ1Reward = 500;
    [SerializeField] private int _cashQ2Reward = 500;
    [Space]
    [SerializeField] private ClientSpeech[] _speeches;
    [Space]
    [SerializeField] private ClientResponse[] _availableResponses;
    [SerializeField] private bool _likesFinalPositiveResponse;

    public string ClientName => _clientName;
    public float SatisfactionThreshold => _satisfactionThreshold;
    public ClientSpeech[] Speeches => _speeches;
    public ClientResponse[] AvailableResponses => _availableResponses;
    public bool LikesFinalPositiveResponse => _likesFinalPositiveResponse;
}

[System.Serializable]
public class ClientResponse
{
    public CallResponse Response;
    public bool LikesIt;
}