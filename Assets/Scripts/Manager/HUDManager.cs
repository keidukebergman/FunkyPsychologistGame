using System;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _victoryGroup;
    [SerializeField] private CanvasGroup _failureGroup;

    internal void ShowFailure()
    {
        _victoryGroup.alpha = 0.0f;
        _failureGroup.alpha = 1.0f;
    }

    internal void ShowVictory()
    {
        _victoryGroup.alpha = 1.0f;
        _failureGroup.alpha = 0.0f;
    }
}
