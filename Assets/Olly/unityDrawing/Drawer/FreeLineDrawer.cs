using System;
using DH.DrawingModule.InputReader;
using DH.DrawingModule.Line;
using UnityEngine;

namespace DH.DrawingModule.Drawer
{
    public class FreeLineDrawer : Drawer
    {
        private Line.Line line;
        private RaycastHit hit;

        public FreeLineDrawer(IInputReader inputReader, LineProperty lineProperty, GameObject linePrefab,
            Camera rayCamera, int canvasLayer) : base(inputReader, lineProperty, linePrefab, rayCamera, canvasLayer)
        {
        }

        protected override void SubscribeInputEvents()
        {
            inputReader.OnDown += OnDown;
            inputReader.OnMove += OnMove;
            inputReader.OnUp += OnUp;
        }

        protected override void UnsubscribeInputEvents()
        {
            inputReader.OnDown -= OnDown;
            inputReader.OnMove -= OnMove;
            inputReader.OnUp -= OnUp;
        }

        private void OnDown(object sender, Vector3 screenPos)
        {
            Ray ray = rayCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out hit, 10000, layerMask))
            {
                // Do something with the object that was hit by the raycast.
                line = lineFactory.GetLine(lineProperty);
                line.UpdateLine(hit.point);

                RaiseLineCreated(line);
            }
        }

        private void OnMove(object sender, Vector3 screenPos)
        {
            Ray ray = rayCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out hit, 10000, layerMask))
            {
                bool lineUpdated = false;
                try
                {
                    lineUpdated = line.UpdateLine(hit.point);
                }
                catch (MissingReferenceException e)
                {
                    line = null;
                }

                if (lineUpdated)
                    RaiseLineDrawn(line, hit.point);
            }
            else
            {
                OnUp(sender, screenPos);
            }
        }

        private void OnUp(object sender, Vector3 screenPos)
        {
            RaiseLineEnded(line);

            line = null;
        }
    }
}