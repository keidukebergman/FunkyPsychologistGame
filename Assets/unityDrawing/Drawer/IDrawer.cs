using System;
using DH.DrawingModule.Line;
using UnityEngine;

namespace DH.DrawingModule.Drawer
{
    public interface IDrawer : IDisposable
    {
        void UpdateLineProperty(LineProperty lineProperty);
        
        Action<Line.Line> OnLineCreated { get; set; }
        Action<Line.Line> OnLineEnded { get; set; }
        Action<Line.Line, Vector3> OnLineSegmentAdded { get; set; }
    }

    public class NullDrawer : IDrawer
    {
        public void Dispose()
        {
        }

        public void UpdateLineProperty(LineProperty lineProperty)
        {
        }

        public Action<Line.Line> OnLineCreated { get; set; }
        public Action<Line.Line> OnLineEnded { get; set; }
        public Action<Line.Line, Vector3> OnLineSegmentAdded { get; set; }
    }
}