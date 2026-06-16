using UnityEngine;

[CreateAssetMenu(fileName = "Speech", menuName = "Scriptables/Client/Speech", order = 1)]
public class ClientSpeech : ScriptableObject
{
    public AudioClip Clip;
    public string Subtitles;
}
