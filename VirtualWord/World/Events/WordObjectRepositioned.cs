using System;
using VirtualWord.WordObjects;
using VirtualWord.World.DataTypes;

namespace VirtualWord.World.Events
{
    public class WordObjectRepositioned : WordObjectEventBase
    {    
        public WordPosition OldPosition { get; private set; }

        public WordObjectRepositioned(WordObject target, WordPosition oldPosition) : base(target)
        {            
            if (oldPosition == null) throw new ArgumentNullException(nameof(oldPosition));
            
            OldPosition = oldPosition;
        }
    }
}