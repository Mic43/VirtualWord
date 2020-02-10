using System;
using VirtualWord.WordObjects;
using VirtualWord.World.DataTypes;

namespace VirtualWord.World.Events
{
    public class WordObjectEventBase
    {
        public WordObject Target { get; private set; }
        public WordPosition TargetPosition => this.Target.Position;

        public WordObjectEventBase(WordObject target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            Target = target;
        }
    }
}