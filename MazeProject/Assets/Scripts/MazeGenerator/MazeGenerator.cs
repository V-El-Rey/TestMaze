using Base;
using Struct;
using UnityEngine;


    internal class MazeGenerator : BaseController
    {
        private MazeSettings _settings;
        
        public MazeSettings Settings { get; set; }
        
        public MazeGenerator(MazeSettings defaultSettings)
        {
            _settings = defaultSettings;
        }
        
        public override void StartExecute()
        {
            base.StartExecute();
            GenerateMaze(_settings);
            Debug.Log("Default maze created");
        }

        public void GenerateMaze(MazeSettings settings)
        {
            _settings = settings;
            Debug.Log($"Maze created: Width/Height : {_settings.Height} / {_settings.Width}");
        }
    }

