using UnityEngine;

namespace Struct
{
    public class Cell
    {
        public CellType Type;

        public GameObject CellPrefab;

        public int XCoordinate;
        public int ZCoordinate;

        public bool Visited;
        
        public Cell(CellType type,GameObject cellPrefab, int xCoordinate, int zCoordinate, bool visited)
        {
            Type = type;
            CellPrefab = cellPrefab;
            XCoordinate = xCoordinate;
            ZCoordinate = zCoordinate;
            Visited = visited;
        }

        public void ResetCell()
        {
            Type = CellType.None;
            CellPrefab = null;
            XCoordinate = 0;
            ZCoordinate = 0;
            Visited = false;
        }
    }
}