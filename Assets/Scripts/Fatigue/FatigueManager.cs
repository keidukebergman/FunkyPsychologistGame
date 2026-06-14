using UnityEngine;

public class FatigueManager : MonoBehaviour
{
    [SerializeField] private float fatigue = 0; //Fatigue as a percentage. Should never go beyond 100 nor below 0
    [SerializeField] private float fatigueGrowthPerSecond = 2; //Fatigue increase as a percentage value per second

    private void Start()
    {
        
    }

    private void Update()
    {
        if (fatigue < 100)
        {
            fatigue += fatigueGrowthPerSecond * Time.deltaTime;
            if (fatigue > 100) fatigue = 100;
        }
    }


    //Getters, setters
    public void SetFatigueGrowthPerSecond(float fatigueGrowthPerSecond)
    {
        this.fatigueGrowthPerSecond = fatigueGrowthPerSecond;
    }

    public void SetFatigue(float fatigue)
    {
        this.fatigue = fatigue;
    }

    public float GetFatigue()
    {
        return this.fatigue;
    }
}

