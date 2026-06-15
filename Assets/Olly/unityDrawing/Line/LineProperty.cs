using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DH.DrawingModule.Line
{
    [Serializable]
    public class LineProperty
    {
        [SerializeField] private float lineWidth;
        [SerializeField] private Color lineColor;

        [FormerlySerializedAs("smootnes")] [SerializeField]
        private float smoothness;

        [SerializeField] private bool useWorldSpace;
        [SerializeField] private Vector3 pointOffsetInWorldCoordinate;

        public float LineWidth
        {
            get { return lineWidth; }
        }

        public Color LineColor
        {
            get { return lineColor; }
        }

        public float Smoothness
        {
            get { return smoothness; }
        }

        public bool UseWorldSpace => useWorldSpace;

        public Vector3 PointOffsetInWorldCoordinate => pointOffsetInWorldCoordinate;

        public LineProperty(float lineWidth, Color lineColor, float smoothness, bool useWorldSpace,
            Vector3 pointOffsetInWorldCoordinate)
        {
            this.lineWidth = lineWidth;
            this.lineColor = lineColor;
            this.smoothness = smoothness;
            this.useWorldSpace = useWorldSpace;
            this.pointOffsetInWorldCoordinate = pointOffsetInWorldCoordinate;
        }
    }
}