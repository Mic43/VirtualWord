using System;
using VirtualWord.Utils;
using VirtualWord.WordObjects;

namespace VirtualWord.World.DataTypes
{
    public class WorldData
    {
        public WordPosition Position { get; private set; }
        public Maybe<WordObject> Occupier { get; private set; }

        public WorldData(WordPosition position, WordObject occupier = null)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));

            Position = position;
            Occupier = new Maybe<WordObject>(occupier);            
        }
        
    }
}