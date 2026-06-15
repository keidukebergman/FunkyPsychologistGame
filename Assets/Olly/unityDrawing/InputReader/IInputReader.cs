using System;
using UnityEngine;

namespace DH.DrawingModule.InputReader
{
    public interface IInputReader : IDisposable
    {
        Action<object, Vector3> OnDown { get; set; }
        Action<object, Vector3> OnUp { get; set; }
        Action<object, Vector3> OnMove { get; set; }

        void Setup();
    }
}