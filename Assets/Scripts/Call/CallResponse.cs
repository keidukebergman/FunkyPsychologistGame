using UnityEngine;

[CreateAssetMenu(fileName = "CallResponse", menuName = "Scriptables/Call Response", order = 0)]
public class CallResponse : ScriptableObject
{
    [SerializeField] private string _response;
    [SerializeField] private SFX _voiceClip;

    public string Response => _response;
    public SFX VoiceClip => _voiceClip;
}
