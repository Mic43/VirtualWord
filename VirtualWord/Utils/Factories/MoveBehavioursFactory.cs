using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.Behaviours.MoveBehaviour;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace VirtualWord.Utils.Factories
{
    public static class MoveBehavioursFactory
    {
        public static IEnumerable<IMoveBehaviour> GetAllMovesUpToDistance(int distance)
        {
            if (distance < 0)
                throw  new ArgumentOutOfRangeException(nameof(distance));

            int size = distance * 2 + 1;
            var moves = new List<DeltaMove>();            

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {                    
                    var vector = new Vector(i-distance,j-distance);
                    if (vector.X!=0 || vector.Y != 0)
                        moves.Add(new DeltaMove(vector));
                    
                }
            }
            return  moves;
        }
        public static IEnumerable<IMoveBehaviour> GetCrossMoves(int distance)
        {
            if (distance < 0)
                throw new ArgumentOutOfRangeException(nameof(distance));
            int size = distance * 2 + 1;
            var moves = new List<DeltaMove>();

            for (int i = 0; i < size; i++)
            {
                if (i - distance != 0)
                {
                    moves.Add(new DeltaMove(new Vector(i - distance, 0)));
                    moves.Add(new DeltaMove(new Vector(0, i - distance)));
                }

            }
            return moves;
        }
        public static IEnumerable<IMoveBehaviour> GetPossibleCrossMoves(int distance,WordSize wordSize)
        {
            if (distance < 0)
                throw new ArgumentOutOfRangeException(nameof(distance));
            return GetCrossMoves(distance).Select(move => new CancelMoveOutOfBoundaries(move, wordSize));
        }
        public static IEnumerable<IMoveBehaviour> GetPossibleUpToDistanceMoves(int distance, WordSize wordSize)
        {
            if (distance < 0)
                throw new ArgumentOutOfRangeException(nameof(distance));
            return GetAllMovesUpToDistance(distance).Select(move => new CancelMoveOutOfBoundaries(move, wordSize));
        }

        public static IEnumerable<IMoveBehaviour> PossibleInWord(this IEnumerable<IMoveBehaviour> moveBehaviours, WordSize wordSize)
        {
            if (moveBehaviours == null) throw new ArgumentNullException(nameof(moveBehaviours));

            return moveBehaviours.Select(move => new CancelMoveOutOfBoundaries(move, wordSize));
        }

        public static IEnumerable<IMoveBehaviour> NonColliding(this IEnumerable<IMoveBehaviour> moveBehaviours, IWorldDataProvider wordDataProvider)
        {
            if (moveBehaviours == null) throw new ArgumentNullException(nameof(moveBehaviours));
            if (wordDataProvider == null) throw new ArgumentNullException(nameof(wordDataProvider));

            return moveBehaviours.Select(move => new CancelCollidingMove(move, wordDataProvider));
        }
    }
}