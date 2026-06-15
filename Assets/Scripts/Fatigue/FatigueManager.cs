using UnityEngine;

public class FatigueManager : MonoBehaviour
{
    public static FatigueManager instance;
    [SerializeField] private float fatigue = 0f; //Fatigue as a percentage. 
    [SerializeField] private float fatigueGrowthPerSecond = 0.02f; //Fatigue increase as a percentage value per second
    bool isDrainingFatigue = false; //Should probably only drain fatigue when talking to clients

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (fatigue < 1.0f && isDrainingFatigue)
        {
            fatigue += fatigueGrowthPerSecond * Time.deltaTime;
            if (fatigue > 1.0f) fatigue = 1.0f;
        }
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
    }

    public void AddFatigue(float fatigue_value)
    {
        fatigue += fatigue_value;
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

