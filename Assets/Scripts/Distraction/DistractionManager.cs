using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using System.Runtime.CompilerServices;

public class DistractionManager : MonoBehaviour
{
    [System.Serializable]
    public class DistractionEntity
    {
        public DistractionEntity (float switchPenalty, float boredness, Interactable interactable)
        {
            this.switchPenalty = switchPenalty;
            this.boredness = boredness;
            this.interactable = interactable;
        }
        public float switchPenalty = 0;
        public float boredness = 0;
        public Interactable interactable;
    }

    public static DistractionManager instance;
    private FatigueManager fatigueManager;
    [SerializeField] private Dictionary<Interactable, DistractionEntity> distractionEntities = new Dictionary<Interactable, DistractionEntity>();
    private Interactable latestInteractable;

    [Header("Effect on active item per interaction")]
    [SerializeField] private float borednessIncreasePerActiveSecond = 0.1f;
    [SerializeField] private float borednessIncreasePerSingleInteraction = 0.2f;
    [Space(10)]
    [Header("Ambient effect")]
    [SerializeField] private float borednessDecreasePerAmbientSecond = 0.1f;
    [Space(10)]
    [Header("Effect on non-active items per interaction")]
    [SerializeField] private float borednessDecreasePerSingleInteraction = 0.3f;
    [SerializeField] private float borednessDecreasePerActiveSecond = 0.3f;

    bool canAffectFatigue = true;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        fatigueManager = FatigueManager.instance;
        foreach (var key in distractionEntities.Keys)
        {
                distractionEntities[key].boredness = Mathf.MoveTowards(distractionEntities[key].boredness, 0, borednessDecreasePerAmbientSecond * Time.deltaTime);
        }
    }

    public void StartCall()
    {
        canAffectFatigue = true;
    }
    public void EndCall()
    {
        canAffectFatigue = false;
    }
    public void Win ()
    {
        canAffectFatigue = false;
    }
    public void Lose()
    {
        canAffectFatigue = false;
    }

    public void OnInteractWithPersistentInteractable(Interactable interactable, float focus_power)
    {
        if (interactable == null) return;
        if (canAffectFatigue != true) return;
        //If we havent seen this interaction item before, add it to the list
        if (!distractionEntities.ContainsKey(interactable)) distractionEntities.Add(interactable, new DistractionEntity(0, 0, interactable));
        DistractionEntity distractionEntity = distractionEntities[interactable];

        //If the interaction item is not the same as the previous, maybe we should penalize the player? Or something.
        //if (interactable != latestInteractable) distractionEntity.switchPenalty = 1;

        latestInteractable = interactable;

        //Remove Fatigue. As boredness with the current item increases, the focus power of the item decreases.
        fatigueManager.RemoveFatigue(focus_power * (1 - distractionEntity.boredness));
        //Up that boredness, dude!
        distractionEntity.boredness = Mathf.MoveTowards(distractionEntity.boredness, 1, borednessIncreasePerActiveSecond * Time.deltaTime);
        //print(distractionEntity.boredness);

        //Remove boredness from other items
        foreach (var key in distractionEntities.Keys)
        {
            if(key.Equals(interactable) == false)
            {
                distractionEntities[key].boredness = Mathf.MoveTowards(distractionEntities[key].boredness, 0, borednessDecreasePerActiveSecond * Time.deltaTime); 
            }
        }
        distractionEntities[interactable] = distractionEntity;
    }

    public void OnInteractWithSingleUseInteractable(Interactable interactable, float focus_power)
    {
        if (interactable == null) return;
        if (canAffectFatigue != true) return;
        //If we havent seen this interaction item before, add it to the list
        if (!distractionEntities.ContainsKey(interactable)) distractionEntities.Add(interactable, new DistractionEntity(0, 0, interactable));
        DistractionEntity distractionEntity = distractionEntities[interactable];

        //If the interaction item is not the same as the previous, maybe we should penalize the player? Or something.
        //if (interactable != latestInteractable) distractionEntity.switchPenalty = 1;

        latestInteractable = interactable;

        //Remove Fatigue. As boredness with the current item increases, the focus power of the item decreases.
        fatigueManager.RemoveFatigue(focus_power * (1 - distractionEntity.boredness));
        //Up that boredness, dude!
        distractionEntity.boredness = Mathf.MoveTowards(distractionEntity.boredness, 1, borednessIncreasePerSingleInteraction);
        print(distractionEntity.boredness);

        //Remove boredness from other items
        foreach (var key in distractionEntities.Keys)
        {
            if (key.Equals(interactable) == false)
            {
                distractionEntities[key].boredness = Mathf.MoveTowards(distractionEntities[key].boredness, 0, borednessDecreasePerSingleInteraction);
            }
        }
        distractionEntities[interactable] = distractionEntity;
    }
}
