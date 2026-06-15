using System;
using System.Collections.Generic;
using DH.DrawingModule.Drawer;
using DH.DrawingModule.Exceptions;
using DH.DrawingModule.Line;
using UnityEngine;

namespace DH.DrawingModule
{
    public class DrawingModule
    {
        private List<Line.Line> lines;

        private int layerMask = -1;

        private IDrawer drawer;

        private bool isActivated;
        private IDrawingModuleSetup setup;

        public LineProperty CurrentLineProperty { get; private set; }

        public Action<Line.Line> LineCreated;
        public Action<Line.Line> LineEnded;
        public Action<Line.Line, Vector3> LineSegmentAdded;

        private DrawerFactory drawerFactory;

        public bool IsActivated
        {
            get { return isActivated; }
        }

        public Type DrawerType
        {
            get { return drawer.GetType(); }
        }

        public DrawingModule(IDrawingModuleSetup setup)
        {
            if (setup == null)
                throw new Exception("Module setup cannot be null");

            lines = new List<Line.Line>();
            this.setup = setup;
            this.drawerFactory = new DrawerFactory(setup.InputReaderFactory);
        }

        public void Activate()
        {
            isActivated = true;
            drawer = new NullDrawer();

            Debug.LogWarning("System activated with null drawer. Remember changing drawer type");
        }

        public void Deactivate()
        {
            if (isActivated)
            {
                isActivated = false;
                drawer.Dispose();
                drawer = new NullDrawer();
            }
        }

        public void ChangeToStraighLine(LineProperty lineProperty)
        {
            if (isActivated)
            {
                drawer.Dispose();
                drawer = drawerFactory.GetStraightLineDrawer(lineProperty, setup);
                drawer.OnLineCreated = OnLineCreated;
                drawer.OnLineEnded = OnLineEnded;
                drawer.OnLineSegmentAdded = delegate(Line.Line line, Vector3 vector3) { LineSegmentAdded?.Invoke(line, vector3); };
                return;
            }

            throw new SystemIsNotActive();
        }

        public void ChangeToFreeLine(LineProperty lineProperty)
        {
            if (isActivated)
            {
                drawer.Dispose();
                drawer = drawerFactory.GetFreeLineDrawer(lineProperty, setup);
                drawer.OnLineCreated = OnLineCreated;
                drawer.OnLineEnded = OnLineEnded;
                drawer.OnLineSegmentAdded = delegate(Line.Line line, Vector3 vector3) { LineSegmentAdded?.Invoke(line, vector3); };
                return;
            }

            throw new SystemIsNotActive();
        }

        private void OnLineCreated(Line.Line line)
        {
            lines.Add(line);

            LineCreated?.Invoke(line);
        }

        void OnLineEnded(Line.Line line)
        {
            LineEnded?.Invoke(line);
        }

        public void UpdateLineProperty(LineProperty lineProperty)
        {
            drawer.UpdateLineProperty(lineProperty);
            CurrentLineProperty = lineProperty;
        }

        public void Undo()
        {
            if (lines.Count > 0)
            {
                Line.Line l = lines[lines.Count - 1];
                lines.Remove(l);
                GameObject.DestroyImmediate(l.gameObject);
            }
        }

        public void Delete(Line.Line l)
        {
            lines.Remove(l);
            GameObject.DestroyImmediate(l.gameObject);
        }

        public void ClearAllLines()
        {
            foreach (Line.Line line in lines)
            {
                GameObject.DestroyImmediate(line.gameObject);
            }

            lines.Clear();
        }

        public void AddLine(Line.Line line)
        {
            lines.Add(line);
        }
    }
}