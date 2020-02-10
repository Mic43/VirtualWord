using System;
using VirtualWord.Behaviours.MoveBehaviour;
using VirtualWord.Behaviours.RotateBehaviour;
using VirtualWord.Utils;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours
{
    public class ObjectMustFaceMoveDirection : IMoveBehaviour,IRotationBehaviour
    {
        private double _angle;
        public IMoveBehaviour MoveBehaviour { get; private set; }
        public ObjectMustFaceMoveDirection(IMoveBehaviour moveBehaviour)
        {
            if (moveBehaviour == null) throw new ArgumentNullException(nameof(moveBehaviour));
            MoveBehaviour = moveBehaviour;
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            var newPosition = MoveBehaviour.GetNewPosition(position);
            _angle = Math.Atan2(newPosition.Y - position.Y, newPosition.X - position.X) + Math.PI;
            return newPosition;
        }

        public double GetNewRotationAngle(double oldDirectionAngle)
        {
            return _angle;
        }
    }
}