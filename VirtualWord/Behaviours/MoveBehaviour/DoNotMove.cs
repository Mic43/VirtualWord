using VirtualWord.Utils;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class DoNotMove : IMoveBehaviour
    {
        public WordPosition GetNewPosition(WordPosition position)
        {
            return position;            
        }
    }
}