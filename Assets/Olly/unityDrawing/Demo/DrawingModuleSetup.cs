using DH.DrawingModule.InputReader;
using UnityEngine;

namespace DH.DrawingModule.TestScripts
{
    public class DrawingModuleSetup : MonoBehaviour, IDrawingModuleSetup
    {
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private Camera rayCamera;
        [SerializeField] private LayerMask canvasLayerMask;

        public GameObject LinePrefab
        {
            get { return linePrefab; }
        }

        public Camera RayCamera
        {
            get { return rayCamera; }
        }

        public IInputReaderFactory InputReaderFactory
        {
            get { return new InputReaderFactory(); }
        }

        public int CanvasLayer
        {
            get { return canvasLayerMask.value; }
        }

    }
}