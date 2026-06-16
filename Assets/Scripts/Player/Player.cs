using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerNod _nod;

    private void Awake()
    {
        _nod = GetComponent<PlayerNod>();
    }


    public bool Nod()
    {
        return _nod.Nod();
    }

}
