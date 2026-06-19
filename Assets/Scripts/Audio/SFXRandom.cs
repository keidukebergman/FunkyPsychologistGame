using UnityEngine;

[CreateAssetMenu(fileName = "SFX Random", menuName = "Scriptables/SFX/Random", order = 2)]
public class SFXRandom : SFXCollection
{
    public static implicit operator SFX(SFXRandom random)
    {
        return random._collection != null && random._collection.Length > 0 ? random._collection[Random.Range(0, random._collection.Length)] : null;
    }
}