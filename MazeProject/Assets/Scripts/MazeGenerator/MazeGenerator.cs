using Base;
using Struct;
using UnityEngine;


    internal class MazeGenerator : BaseController
    {
        private MazeSettings _settings;
        
        public MazeSettings Settings { get; set; }
        
        public MazeGenerator()
        {
        }
        
        public override void StartExecute()
        {
            base.StartExecute();
            Debug.Log("Default maze created");
        }

        public void GenerateMaze(MazeSettings settings)
        {
            Debug.Log($"Maze created: Width/Height : {settings.Height} / {settings.Width}");
        }
    }

