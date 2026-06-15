using DH.DrawingModule.Line;
using UnityEngine;

namespace DH.DrawingModule.TestScripts
{
    public class TestDrawing : MonoBehaviour
    {
        [SerializeField] private DrawingModuleSetup setup;

        private DrawingModule module;

        private void Start()
        {
            module = new DrawingModule(setup);
            Activate();
        }

        private void Update()
        {/*
            if (Input.GetKeyDown(KeyCode.C))
                ClearAll();

            if (Input.GetKeyDown(KeyCode.A))
                Activate();

            if (Input.GetKeyDown(KeyCode.D))
                Deactivate();

            if (Input.GetKeyDown(KeyCode.U))
                ClearLast();

            if (Input.GetKeyDown(KeyCode.F))
                FreeDraw();

            if (Input.GetKeyDown(KeyCode.S))
                StraightDraw();*/
            
        }

        void Activate()
        {
            module.Activate();
            module.ChangeToFreeLine(new LineProperty(0.5f, Color.yellow, 0.2f, true, Vector3.zero));
        }

        void Deactivate()
        {
            module.Deactivate();
        }

        void ClearAll()
        {
            module.ClearAllLines();
        }

        void ClearLast()
        {
            module.Undo();
        }

        void FreeDraw()
        {
            module.ChangeToFreeLine(new LineProperty(0.5f, Color.yellow, 0.2f, true, Vector3.zero));
        }

        void StraightDraw()
        {
            module.ChangeToStraighLine(new LineProperty(0.5f, Color.yellow, 0.2f, true, Vector3.zero));
        }
    }
}