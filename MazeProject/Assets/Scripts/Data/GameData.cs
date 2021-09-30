using UI;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/GameData", fileName = "GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public UIView UI;
    }
}
