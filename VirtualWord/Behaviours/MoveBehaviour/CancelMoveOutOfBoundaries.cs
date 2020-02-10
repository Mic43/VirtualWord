using System;
using VirtualWord.Utils;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class CancelMoveOutOfBoundaries : IMoveBehaviour
    {
        public IMoveBehaviour MoveBehaviour { get; }
        public WordSize WordSize { get; set; }

        public CancelMoveOutOfBoundaries(IMoveBehaviour moveBehaviour,WordSize wordSize)
        {
            if (moveBehaviour == null) throw new ArgumentNullException(nameof(moveBehaviour));

            MoveBehaviour = moveBehaviour;
            WordSize = wordSize;
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            var newWordPosition = MoveBehaviour.GetNewPosition(position);
            return newWordPosition.IsValidInWord(WordSize) ? 
                newWordPosition : position;
        }
    }
}