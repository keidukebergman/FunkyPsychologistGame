using System;
using UnityEngine;

namespace DH.DrawingModule.InputReader
{
    public class SceneInputReader : MonoBehaviour, IInputReader
    {
        public virtual Action<object, Vector3> OnDown { get; set; }
        public virtual Action<object, Vector3> OnUp { get; set; }
        public virtual Action<object, Vector3> OnMove { get; set; }
        
        public void Setup()
        {
        }

        public void Dispose()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}