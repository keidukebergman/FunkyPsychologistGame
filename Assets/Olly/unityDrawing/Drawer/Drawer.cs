using System;
using DH.DrawingModule.InputReader;
using DH.DrawingModule.Line;
using UnityEngine;

namespace DH.DrawingModule.Drawer
{
    public abstract class Drawer : IDrawer
    {
        protected LineFactory lineFactory;
        protected LineProperty lineProperty;
        protected IInputReader inputReader;
        protected int layerMask;
        protected Camera rayCamera;

        public Drawer(IInputReader inputReader, LineProperty lineProperty, GameObject linePrefab, Camera rayCamera,
            int canvasLayer)
        {
            lineFactory = new LineFactory(linePrefab);
            layerMask = canvasLayer;

            this.rayCamera = rayCamera;
            this.inputReader = inputReader;

            UpdateLineProperty(lineProperty);
            SubscribeInputEvents();
        }

        public void Dispose()
        {
            UnsubscribeInputEvents();
            inputReader.Dispose();
            OnLineCreated = null;
        }

        protected void RaiseLineCreated(Line.Line line)
        {
            OnLineCreated?.Invoke(line);
        }

        protected void RaiseLineEnded(Line.Line line)
        {
            OnLineEnded?.Invoke(line);
        }
        
        protected void RaiseLineDrawn(Line.Line line, Vector3 position)
        {
            OnLineSegmentAdded?.Invoke(line, position);
        }

        public void UpdateLineProperty(LineProperty lineProperty)
        {
            this.lineProperty = lineProperty;
        }

        public Action<Line.Line> OnLineCreated { get; set; }
        public Action<Line.Line> OnLineEnded { get; set; }
        public Action<Line.Line, Vector3> OnLineSegmentAdded { get; set; }

        protected abstract void SubscribeInputEvents();
        protected abstract void UnsubscribeInputEvents();
    }
}