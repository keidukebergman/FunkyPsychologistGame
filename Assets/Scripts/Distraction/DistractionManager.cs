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
    [SerializeField] private Dictionary<Interactable, DistractionEntity> distractionEntities = new Dictionary<Interactable, DistractionEntity>();
    private Interactable latest_interactable;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    public void OnInteractWithInteractable(Interactable interactable, float focus_power)
    {
        if (interactable == null) return;
        if (!distractionEntities.ContainsKey(interactable)) distractionEntities.Add(interactable, new DistractionEntity(0, 0, interactable));
        DistractionEntity distractionEntity = distractionEntities[interactable];
        if (interactable != latest_interactable) distractionEntity.switchPenalty = 1;
    }

}
