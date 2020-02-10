using System;

namespace VirtualWord.World.DataTypes
{
    public struct WordSize
    {
        public int X { get;  }
        public int Y { get; }

        public WordSize(int x, int y)
        {
            if (!ValidatePosition(x, y))
                throw new ArgumentOutOfRangeException();
            X = x;
            Y = y;
        }


        private static bool ValidatePosition(int x, int y)
        {
            return x > -1 && y > -1;
        }
    }
}