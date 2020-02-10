using VirtualWord.WordObjects;
using VirtualWord.World.DataTypes;

namespace VirtualWord.World.Events
{
    public class WordObjectInserted : WordObjectEventBase
    {
        public WordObjectInserted(WordObject target) : base(target)
        {
        }
    }
}