using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DH.DrawingModule.Line
{
	public class Line : MonoBehaviour {

		public LineRenderer lineRenderer;
		public int LineCount
		{
			get { return points.Count; }
		}

		List<Vector3> points = new List<Vector3>(8);

		public List<Vector3> Points => points;

		private LineProperty lineProperty;
		private float length;

		public float Length => length;

		public void Clear()
		{
			points.Clear();
			length = 0;
			lineRenderer.positionCount = 0;
		}

		public void UpdateLineRenderer(LineProperty lineProperty)
		{
			this.lineProperty = lineProperty;
			
			lineRenderer.startWidth = lineProperty.LineWidth;
			lineRenderer.endWidth = lineProperty.LineWidth;
			lineRenderer.startColor = (lineProperty.LineColor);
			lineRenderer.endColor = (lineProperty.LineColor);
			lineRenderer.useWorldSpace = lineProperty.UseWorldSpace;
		}

		public bool UpdateLine (Vector3 mousePos)
		{
			if (points.Count == 0)
			{
				SetPoint(mousePos + lineProperty.PointOffsetInWorldCoordinate);
				return true;
			}

			if (Vector3.Distance(points.Last(), mousePos) > lineProperty.Smoothness)
			{
				SetPoint(mousePos + lineProperty.PointOffsetInWorldCoordinate);
				return true;
			}

			return false;
		}

		void SetPoint (Vector3 point)
		{
			points.Add(point);

			lineRenderer.positionCount = points.Count;
			lineRenderer.SetPosition(points.Count - 1, point);
			
			CalculateLength(point);
		}

		public void SetLastPoint(Vector3 point)
		{
			if (points.Count < 2)
			{
				UpdateLine(point);
			}
		
			points[points.Count - 1] = point;

			lineRenderer.positionCount = points.Count;
			lineRenderer.SetPosition(points.Count - 1, point);
		}

		void CalculateLength(Vector3 point)
		{
			if (points.Count <= 1)
				return;

			length += (point - points[points.Count - 2]).magnitude;
		}
	}
}


