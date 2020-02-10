using VirtualWord.Utils;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.WordObjects.Interfaces
{
    public interface IPositionable
    {
        WordPosition Position { get; }
    }
}