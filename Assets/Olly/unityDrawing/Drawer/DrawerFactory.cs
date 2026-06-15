using DH.DrawingModule.InputReader;
using DH.DrawingModule.Line;
using UnityEngine;

namespace DH.DrawingModule.Drawer
{
    public class DrawerFactory
    {
        private IInputReaderFactory inputReaderFactory;

        public DrawerFactory(IInputReaderFactory inputReaderFactory)
        {
            this.inputReaderFactory = inputReaderFactory;
        }

        public IDrawer GetStraightLineDrawer(LineProperty lineProperty, IDrawingModuleSetup setup)
        {
            IInputReader inputReader = inputReaderFactory.GetInputReader();
            inputReader.Setup();
            return new StraightLineDrawer(inputReader, lineProperty, setup.LinePrefab, setup.RayCamera, setup.CanvasLayer);
        }

        public IDrawer GetFreeLineDrawer(LineProperty lineProperty, IDrawingModuleSetup setup)
        {
            IInputReader inputReader = inputReaderFactory.GetInputReader();
            inputReader.Setup();
            return new FreeLineDrawer(inputReader, lineProperty, setup.LinePrefab, setup.RayCamera, setup.CanvasLayer);
        }
    }
}