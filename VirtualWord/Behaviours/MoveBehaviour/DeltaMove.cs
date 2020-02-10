using VirtualWord.Utils;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class DeltaMove : IMoveBehaviour
    {
        private readonly Vector _delta;

        public DeltaMove(Vector delta)
        {
            _delta = delta;
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            return new WordPosition(position.X + _delta.X, position.Y + _delta.Y);
        }
    }
}