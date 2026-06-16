using UnityEngine;

public class FatigueManager : MonoBehaviour
{
    public static FatigueManager instance;
    [SerializeField] private float fatigue = 0f; //Fatigue as a percentage. 
    [SerializeField] private float fatigueGrowthPerSecond = 0.02f; //Fatigue increase as a percentage value per second
    [SerializeField] private float fatigueRestorationPerSecond = 0.04f; //Fatigue should be reset between clients
    bool isDrainingFatigue = true; //Should probably only drain fatigue when talking to clients
    bool isRestoringFatigue = false;
    //bool canAffectFatigue = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (fatigue < 1.0f && isDrainingFatigue)
        {
            fatigue += fatigueGrowthPerSecond * Time.deltaTime;
            //Oooh buddy, time to suffer
            if (fatigue >= 1.0f)
            {
                fatigue = 1.0f;
                FallAsleep();
            }
        }
        if (fatigue > 0.0f && isRestoringFatigue)
        {
            fatigue += fatigueRestorationPerSecond * Time.deltaTime;
            if(fatigue <= 0.0f)
            {
                fatigue = 0.0f;
            }
        }
    }

    private void FallAsleep()
    {
        DistractionManager.instance.Lose();
        isDrainingFatigue = false;
        print("You lost! Sleepy time!");
    }

    public void EndCall()
    {
        isDrainingFatigue = false;
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

