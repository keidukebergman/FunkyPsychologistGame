using System;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private HUDChoice _choiceGroup;
    [Space]
    [SerializeField] private CanvasGroup _victoryGroup;
    [SerializeField] private CanvasGroup _failureGroup;

    public void ShowFailure()
    {
        _victoryGroup.alpha = 0.0f;
        _failureGroup.alpha = 1.0f;
    }

    public void ShowVictory()
    {
        _victoryGroup.alpha = 1.0f;
        _failureGroup.alpha = 0.0f;
    }

    public void OnSpeechFinished(int progress, string[] choices)
    {
        _choiceGroup.PresentChoices(choices);
    }
    public void OnChoiceMade()
    {
        _choiceGroup.OnChoiceMade();
    }
}
