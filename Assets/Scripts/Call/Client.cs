using UnityEngine;

[CreateAssetMenu(fileName = "Client", menuName = "Scriptables/Client/New Client", order = 0)]
public class Client : ScriptableObject
{
    [SerializeField] private ClientSpeech[] _speeches;
    [SerializeField] private float _satisfactionThreshold = .8f;
    [SerializeField] private bool _likesPositiveResponse;

    public ClientSpeech[] Speeches => _speeches;
    public float SatisfactionThreshold => _satisfactionThreshold;
    public bool LikesPositiveResponse => _likesPositiveResponse;
}
