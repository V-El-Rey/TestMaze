using CameraControl;
using Data;
using PathfindingController;
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
        public PlayerView player;
        public LineDrawerView lineDrawerView;

        private MazeSettings _defaultMazeSettings;
        private MazePrefabs _mazePrefabs;

        private UIController _uiController;
        private MazeGenerator _mazeGenerator;
        private PoolManager _poolManager;
        private CameraController _cameraController;
        private InputController _inputController;
        private PlayerController _playerController;
        private Pathfinder _pathfinder;

        void Start()
        {
            _poolManager = new PoolManager();
            _uiController = new UIController(gameData.UI, rootUI);
            _defaultMazeSettings = new MazeSettings(gameData.width, gameData.height);
            _mazePrefabs = new MazePrefabs(gameData.wallPrefab, gameData.cellPrefab);
            _mazeGenerator = new MazeGenerator(_defaultMazeSettings, _mazePrefabs, rootMaze);
            _cameraController = new CameraController(cameraView);
            _inputController = new InputController(cameraView.Camera, gameData);
            _playerController = new PlayerController(player, lineDrawerView, gameData);
            _pathfinder = new Pathfinder();
            
            
            
            _poolManager.InitializePool(objectPool);

            _uiController.GetSettings += _mazeGenerator.SpawnMaze;
            _mazeGenerator.SendSpawnPointCoordinate += _playerController.GetRandomCoordinatesAndSpawnPlayer;
            _mazeGenerator.SendCellsArray += _pathfinder.SetCellsArray;
            _inputController.SendTargetPositionToPlayer += _playerController.SetTargetPosition;
            _playerController.StartPathfinding += _pathfinder.SetStartAndTarget;
            _pathfinder.SendPath += _playerController.SetPath;


            #region StartExecute

             _uiController.StartExecute();
             _mazeGenerator.StartExecute();
             _playerController.StartExecute();

            #endregion
        }
        
        void Update()
        {
            _uiController.UpdateExecute();
            _inputController.UpdateExecute();
            _playerController.UpdateExecute();
            _pathfinder.UpdateExecute();
        }
    }
}
