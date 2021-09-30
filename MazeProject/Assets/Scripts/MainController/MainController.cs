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

        private MazeSettings _defaultMazeSettings;

        private UIController _uiController;
        private MazeGenerator _mazeGenerator;
        private PoolManager _poolManager;

        void Start()
        {
            _poolManager = new PoolManager();
            _uiController = new UIController(gameData.UI, rootUI);
            _defaultMazeSettings = new MazeSettings(gameData.width, gameData.height);
            _mazeGenerator = new MazeGenerator(_defaultMazeSettings);
            
            
            _poolManager.InitializePool(objectPool, gameData.cellPrefab);

            _uiController.GetSettings += _mazeGenerator.GenerateMaze;
            
            
            #region StartExecute

             _uiController.StartExecute();
             _mazeGenerator.StartExecute();

            #endregion
        }
        
        void Update()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     PoolManager.GetCellFromPool(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            // }
        }
    }
}
