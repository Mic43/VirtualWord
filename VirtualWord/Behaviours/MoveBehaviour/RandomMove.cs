using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.Utils;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class RandomMove : IMoveBehaviour 
    {
        private readonly IEnumerable<IMoveBehaviour> _moveBehaviours;
        private readonly Random _random;

        public RandomMove(IEnumerable<IMoveBehaviour> moveBehaviours)
        {
            if (moveBehaviours == null) throw new ArgumentNullException(nameof(moveBehaviours));
            _moveBehaviours = moveBehaviours;
            _random = new Random();
        }

        public RandomMove(params IMoveBehaviour[] moveBehaviours) : this(new List<IMoveBehaviour>(moveBehaviours))
        {
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            return _moveBehaviours.ElementAt(_random.Next(_moveBehaviours.Count())).GetNewPosition(position);            
        }
    }
}