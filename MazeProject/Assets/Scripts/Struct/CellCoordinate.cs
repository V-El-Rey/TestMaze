using System;

namespace Struct
{
    public struct CellCoordinate : IComparable<CellCoordinate>
    {
        public int xCoordinate;
        public int yCoordinate;

        public CellCoordinate(int x, int y)
        {
            xCoordinate = x;
            yCoordinate = y;
        }

        public int CompareTo(CellCoordinate other)
        {
            if (this.xCoordinate > other.xCoordinate && this.yCoordinate > other.yCoordinate)
            {
                return 1;
            }

            if (this.xCoordinate < other.xCoordinate && this.yCoordinate < other.yCoordinate)
            {
                return -1;
            }

            if (this.xCoordinate == other.yCoordinate && this.yCoordinate == other.yCoordinate)
            {
                return 0;
            }

            return -1;
        }
    }
}