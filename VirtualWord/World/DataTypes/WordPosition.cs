using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.Utils;
using VirtualWord.WordObjects;

namespace VirtualWord.World.DataTypes
{
    public class WordPosition : ICloneable
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public WordPosition()
        {
            X = 0;
            Y = 0;
        }

        public WordPosition(WordPosition wordPosition):this(wordPosition.X,wordPosition.Y)
        {            
        }
        public WordPosition(int x, int y)
        {
            //if (!ValidatePosition(x, y))
            //  throw new ArgumentException();
            X = x;
            Y = y;
        }

        private bool ValidatePosition(int x, int y)
        {
            return x > -1 && y > -1;
        }

        public bool IsValidInWord(WordSize wordSize)
        {
            return X < wordSize.X && Y < wordSize.Y && ValidatePosition(X,Y);
        }

        protected bool Equals(WordPosition other)
        {
            return X == other.X && Y == other.Y;
        }

        public double DistanceTo(WordPosition position)
        {
            var vector = new Vector(position.X - X ,position.Y - Y);
            return Math.Max(Math.Abs(vector.X), Math.Abs(vector.Y));
            //return Math.Sqrt(vector.X*vector.X + vector.Y*vector.Y);
        }

        public Maybe<WordPosition> GetNearest(IEnumerable<WordPosition> availablePositions)
        {
            if (availablePositions == null) throw new ArgumentNullException(nameof(availablePositions));

            return new Maybe<WordPosition>(availablePositions.Select(x => new {Distance = x.DistanceTo(this), Position = x})
                    .Where(x => x.Distance != 0)
                    .OrderBy(x => x.Distance)
                    .Select(x => x.Position)
                    .FirstOrDefault());
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WordPosition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public object Clone()
        {
            return new WordPosition(X,Y);
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}