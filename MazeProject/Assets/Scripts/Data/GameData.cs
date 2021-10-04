using UI;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/GameData", fileName = "GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public UIView UI;
        public GameObject cellPrefab;
        public GameObject wallPrefab;
        
        [Header("DefaultMazeSettings")] 
        public int width;
        public int height;

        [Header("Other settings")] 
        public float dragThreshold;
        public float dragSensitivity;
        public float movementStepDelay;
    }
}
