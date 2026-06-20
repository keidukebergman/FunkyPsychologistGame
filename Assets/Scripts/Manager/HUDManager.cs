using System;
using Unity.VisualScripting;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private HUDChoice _choiceGroup;
    [Space]
    [SerializeField] private CanvasGroup _victoryGroup;
    [SerializeField] private CanvasGroup _failureGroup;

    [SerializeField] private Animator _introAnimator;

    private readonly int PLAY_ID = Animator.StringToHash("Play");

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

    public void PlayIntro()
    {
        _introAnimator.SetTrigger(PLAY_ID);
    }
}
