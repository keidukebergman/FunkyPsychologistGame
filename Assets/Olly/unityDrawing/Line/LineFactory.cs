using System;
using UnityEngine;

namespace DH.DrawingModule.Line
{
    public class LineFactory
    {
        private GameObject linePrefab;
        
        public LineFactory(GameObject linePrefab)
        {
            this.linePrefab = linePrefab;
            
            ValidateLinePrefab();
        }

        void ValidateLinePrefab()
        {
            if(linePrefab.GetComponent<Line>() == null)
                throw new Exception("Line prefab does not have line component on it");
        }
        
        public Line GetLine(LineProperty lineProperty)
        {
            GameObject lineGameObject = GameObject.Instantiate(linePrefab);
            Line line = lineGameObject.GetComponent<Line>();
            
            line.UpdateLineRenderer(lineProperty);

            return line;
        }
    }
}