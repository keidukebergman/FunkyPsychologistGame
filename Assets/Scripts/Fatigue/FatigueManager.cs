using UnityEngine;
using System.Collections;
using NUnit.Framework.Constraints;
using System;

public class FatigueManager : MonoBehaviour
{
    public static FatigueManager instance;
    [SerializeField] private float fatigue = 0f; //Fatigue as a percentage. 
    [SerializeField] private float fatigueGrowthPerSecond = 0.02f; //Fatigue increase as a percentage value per second
    [SerializeField] private float fatigueRestorationPerSecond = 0.04f; //Fatigue should be reset between clients
    bool isDrainingFatigue = true; //Should probably only drain fatigue when talking to clients
    bool isRestoringFatigue = false;
    [SerializeField] private State state;
    enum State
    {
        Idle,
        InCall,
        Lost
    }

    [SerializeField] private float sleepTime = 3;

    public Action OnSleep;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

  
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (fatigue > 0.0f)
                {
                    fatigue -= fatigueRestorationPerSecond * Time.deltaTime;
                    if (fatigue <= 0.0f)
                    {
                        fatigue = 0.0f;
                    }
                }
                break;

            case State.InCall:
                if (fatigue < 1.0f)
                {
                    fatigue += fatigueGrowthPerSecond * Time.deltaTime;
                    //Oooh buddy, time to suffer
                    if (fatigue >= 1.0f)
                    {
                        fatigue = 1.0f;
                        FallAsleep();
                    }
                }
                break;

            case State.Lost:
                if (fatigue > 0.0f)
                {
                    fatigue -= fatigueRestorationPerSecond * Time.deltaTime;
                    if (fatigue <= 0.0f)
                    {
                        fatigue = 0.0f;
                    }
                }
                break;
        }
    }

    private void FallAsleep()
    {
        DistractionManager.instance.Lose();
        isDrainingFatigue = false;
        print("You lost! Sleepy time!");
        OnSleep?.Invoke();
        StartCoroutine(SleepyTime());
    }

    private void Awaken()
    {
        state = State.Lost;
    }

    IEnumerator SleepyTime()
    {
        yield return new WaitForSeconds(sleepTime);
        Awaken();
    }

    public void EnterCall()
    {
        state = State.InCall;
    }
    public void EndCall()
    {
        state = State.Idle;
    }


    //Getters, setters, modifiers
    public void SetFatigueGrowthPerSecond(float fatigueGrowthPerSecond)
    {
        this.fatigueGrowthPerSecond = fatigueGrowthPerSecond;
    }

    public void SetFatigue(float fatigue)
    {
        this.fatigue = fatigue;
    }

    public void RemoveFatigue(float fatigue_value)
    {
        fatigue -= fatigue_value;

        if (fatigue < 0)
        {
            fatigue = 0;
        }
    }

    public void AddFatigue(float fatigue_value)
    {
        fatigue += fatigue_value;

        if (fatigue > 1)
        {
            fatigue = 1;
        }
    }

    public void SetFatigueDrain(bool should_drain)
    {
        isDrainingFatigue = should_drain;
    }

    public float GetFatigue()
    {
        return this.fatigue;
    }
}

