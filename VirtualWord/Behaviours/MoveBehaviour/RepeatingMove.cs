using System;
using VirtualWord.Utils;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class RepeatingMove : IMoveBehaviour
    {
        private readonly IMoveBehaviour _moveBehaviourToRepeat;
        private readonly int _count;

        public RepeatingMove(IMoveBehaviour moveBehaviourToRepeat,int count)
        {
            if (moveBehaviourToRepeat == null) throw new ArgumentNullException(nameof(moveBehaviourToRepeat));
            if( count < 0 ) throw  new ArgumentOutOfRangeException(nameof(count));

            _moveBehaviourToRepeat = moveBehaviourToRepeat;
            _count = count;
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            WordPosition finalPosition = position;
            for (int i = 0; i < _count; i++)
            {
                finalPosition = _moveBehaviourToRepeat.GetNewPosition(finalPosition);
            }
            return finalPosition;
        }
    }
}