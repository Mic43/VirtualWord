using VirtualWord.World.DataTypes;

namespace VirtualWord.World.Interfaces
{
    public interface IWorldDataProvider
    {
        WorldData Get(WordPosition position);
    }
}