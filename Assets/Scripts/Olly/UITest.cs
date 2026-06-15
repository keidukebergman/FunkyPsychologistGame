using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UITest : MonoBehaviour
{
    void Update()
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;

        List<RaycastResult> results = new();

        EventSystem.current.RaycastAll(data, results);

        foreach (var r in results)
        {
            Debug.Log($"{r.gameObject.name} depth={r.depth}");
        }
    }
}