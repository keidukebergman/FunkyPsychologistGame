using UnityEngine;
using System;

namespace DH.DrawingModule.InputReader
{
    public class InputReaderFactory : IInputReaderFactory
    {
        public IInputReader GetInputReader()
        {
            GameObject inputReaderObject = new GameObject("InputReader");
            
#if UNITY_WEBGL || UNITY_EDITOR
            return inputReaderObject.AddComponent<MouseSceneInputReader>();
#elif !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
	    throw new NotImplementedException();
#endif
        }
    }
}
