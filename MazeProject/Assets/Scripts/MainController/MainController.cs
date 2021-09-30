using Data;
using Struct;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace MainController
{
    public class MainController : MonoBehaviour
    {
        public GameData gameData;
        public GameObject rootUI;
        

        private UIController _uiController;
        private MazeGenerator _mazeGenerator;

        void Start()
        {
            _uiController = new UIController(gameData.UI, rootUI);
            _mazeGenerator = new MazeGenerator();
            _uiController.GetSettings += _mazeGenerator.GenerateMaze;

            
            #region StartExecute

             _uiController.StartExecute();
             _mazeGenerator.StartExecute();

            #endregion
        }
        
        void Update()
        {
        }
    }
}
