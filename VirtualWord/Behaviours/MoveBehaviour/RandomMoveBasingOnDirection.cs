using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.Utils;
using VirtualWord.WordObjects;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class RandomMoveBasingOnDirection : IMoveBehaviour
    {
        private readonly IRotatable _wordObject;
        private readonly IEnumerable<IMoveBehaviour> _moveBehaviours;
        private readonly Random _random;

        public RandomMoveBasingOnDirection(IRotatable wordObject, IEnumerable<IMoveBehaviour> moveBehaviours,Random random)
        {
            if (wordObject == null) throw new ArgumentNullException(nameof(wordObject));
            if (moveBehaviours == null) throw new ArgumentNullException(nameof(moveBehaviours));

            _wordObject = wordObject;
            _moveBehaviours = moveBehaviours;
            _random = random;
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            var newPositions = _moveBehaviours.Select(m=>m.GetNewPosition(position))
                                               .Where(x=>!x.Equals(position)).ToList();

            var posAndAngles = newPositions.Select(newPosition => 
                new
                {
                    Position = newPosition,
                    Angle = Math.Abs(Math.Atan2(newPosition.Y - position.Y, newPosition.X - position.X) + Math.PI
                                            - _wordObject.DirectionAngle)
                }).ToList();

            var onlyInFieldOfView = posAndAngles.Where(x => x.Angle <= Math.PI/2 + 0.001).ToList();
            int ran = _random.Next(10);

            if (ran <= 1)
                return onlyInFieldOfView.DefaultIfEmpty(posAndAngles.First()).ToList()[_random.Next(onlyInFieldOfView.Count)].Position;
            else
                return onlyInFieldOfView.OrderBy(x => x.Angle).DefaultIfEmpty(posAndAngles.First()).First().Position;


        }
    }
}