using VirtualWord.Utils;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public interface IMoveBehaviour 
    {
        WordPosition GetNewPosition( WordPosition position);
    }
}