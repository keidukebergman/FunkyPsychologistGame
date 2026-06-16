using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private Client[] clients;

    private List<Client> _waitingClients;
    private List<Client> _finishedClients;

    public bool NoMoreClients => _waitingClients.Count == 0;

    public Client CurrentClient { get; private set; }

    private void Awake()
    {
        _waitingClients = new List<Client>();
        _waitingClients.AddRange(clients);
    }


    public Client PullClient() 
    {
        int n = Random.Range(0, _waitingClients.Count - 1);
        CurrentClient = _waitingClients[n];
        _waitingClients.RemoveAt(n);
        return CurrentClient;
    }

    public bool ConfirmClientIsSatisfied(float p_callSatisfaction)
    {
        return p_callSatisfaction >= CurrentClient.SatisfactionThreshold;
    }
}
