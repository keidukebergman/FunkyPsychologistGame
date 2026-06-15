using DH.DrawingModule.InputReader;
using UnityEngine;

namespace DH.DrawingModule
{
    public interface IDrawingModuleSetup
    {
        GameObject LinePrefab { get; }
        Camera RayCamera { get; }
        IInputReaderFactory InputReaderFactory { get; }
        int CanvasLayer { get; }
    }
}