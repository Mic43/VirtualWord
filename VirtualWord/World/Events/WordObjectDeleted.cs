using VirtualWord.WordObjects;

namespace VirtualWord.World.Events
{
    public class WordObjectDeleted : WordObjectEventBase
    {
        public WordObjectDeleted(WordObject target) : base(target)
        {
        }
    }
}