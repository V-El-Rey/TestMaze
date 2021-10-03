using UnityEngine;

namespace Struct
{
    public struct MazePrefabs
    {
        public GameObject Wall;
        public GameObject Cell;

        public MazePrefabs(GameObject wall, GameObject cell)
        {
            Wall = wall;
            Cell = cell;
        }
    }
}