using UI;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/GameData", fileName = "GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public UIView UI;
        public GameObject cellPrefab;

        [Header("DefaultMazeSettings")] 
        public int width;
        public int height;
    }
}
