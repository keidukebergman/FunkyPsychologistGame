using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private Client[] clients;

    private List<Client> _waitingClients;
    private List<Client> _finishedClients;

    public bool NoMoreClients => _waitingClients.Count == 0;

    private void Awake()
    {
        _waitingClients = new List<Client>();
        _waitingClients.AddRange(clients);
    }


    public Client PullClient() 
    {
        return _waitingClients[Random.Range(0,_waitingClients.Count - 1)];
    }
}
