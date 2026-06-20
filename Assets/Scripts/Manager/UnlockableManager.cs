using System;
using UnityEngine;

public class UnlockableManager : MonoBehaviour
{
    private GameObject[] _unlockables;

    private void Awake()
    {
        CollectChildren();
    }

    private void Start()
    {
        DisableAll();
    }

    private void CollectChildren()
    {
        _unlockables = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _unlockables[i] = transform.GetChild(i).gameObject;
        }
    }

    private void DisableAll()
    {
        foreach (GameObject obj in _unlockables)
        {
            obj.SetActive(false);
        }
    }

    public void Restore()
    {
        DisableAll();
    }
}
