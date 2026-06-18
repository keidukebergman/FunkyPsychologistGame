using UnityEngine;
using System;

namespace DH.DrawingModule.InputReader
{
    public class InputReaderFactory : IInputReaderFactory
    {
        public IInputReader GetInputReader()
        {
            GameObject inputReaderObject = new GameObject("InputReader");
            
            return inputReaderObject.AddComponent<MouseSceneInputReader>();
        }
    }
}
