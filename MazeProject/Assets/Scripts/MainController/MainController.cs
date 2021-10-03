using CameraControl;
using Data;
using Struct;
using UI;
using UnityEngine;

namespace MainController
{
    public class MainController : MonoBehaviour
    {
        public GameData gameData;
        public GameObject rootUI;
        public Transform objectPool;
        public Transform rootMaze;
        public CameraView cameraView;

        private MazeSettings _defaultMazeSettings;
        private MazePrefabs _mazePrefabs;

        private UIController _uiController;
        private MazeGenerator _mazeGenerator;
        private PoolManager _poolManager;
        private CameraController _cameraController;
        private InputController _inputController;

        void Start()
        {
            _poolManager = new PoolManager();
            _uiController = new UIController(gameData.UI, rootUI);
            _defaultMazeSettings = new MazeSettings(gameData.width, gameData.height);
            _mazePrefabs = new MazePrefabs(gameData.wallPrefab, gameData.cellPrefab);
            _mazeGenerator = new MazeGenerator(_defaultMazeSettings, _mazePrefabs, rootMaze);
            _cameraController = new CameraController(cameraView);
            _inputController = new InputController(cameraView.Camera);
            
            
            _poolManager.InitializePool(objectPool);

            _uiController.GetSettings += _mazeGenerator.SpawnMaze;


            #region StartExecute

             _uiController.StartExecute();
             _mazeGenerator.StartExecute();

            #endregion
        }
        
        void Update()
        {
            _uiController.UpdateExecute();
            _inputController.UpdateExecute();
        }
    }
}
