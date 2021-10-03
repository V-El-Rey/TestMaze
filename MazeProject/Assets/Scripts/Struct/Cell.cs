using UnityEngine;

namespace Struct
{
    public class Cell
    {
        public CellType Type;

        public GameObject CellPrefab;

        public int XCoordinate;
        public int YCoordinate;

        public bool Visited;
        
        public Cell(CellType type,GameObject cellPrefab, int xCoordinate, int yCoordinate, bool visited)
        {
            Type = type;
            CellPrefab = cellPrefab;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Visited = visited;
        }

        public void ResetCell()
        {
            Type = CellType.None;
            CellPrefab = null;
            XCoordinate = 0;
            YCoordinate = 0;
            Visited = false;
        }
    }
}