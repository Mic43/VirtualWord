using System;
using VirtualWord.WordObjects;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Events;
using VirtualWord.World.Interfaces;

namespace VirtualWord.World
{
    public class WorldDataStorer :IWorldDataProvider,
        Utils.IObserver<WordObjectInserted>, 
        Utils.IObserver<WordObjectDeleted>, 
        Utils.IObserver<WordObjectRepositioned>

    {
        private readonly WordSize _wordSize;
        private readonly WorldData[,] _wordData;

        private void CreateDefaultWorldData()
        {
            for (int i = 0; i < _wordSize.X; i++)
            {
                for (int j = 0; j < _wordSize.Y; j++)
                {
                    _wordData[i, j] = new WorldData(new WordPosition(i, j));
                }
            }
        }
        private void Set(WordPosition position, WordObject wordObject)
        {
            _wordData[position.X, position.Y] = new WorldData(position, wordObject);
        }

        public WorldDataStorer(WordSize wordSize)
        {
            _wordSize = wordSize;
            _wordData = new WorldData[wordSize.X, wordSize.Y];
            CreateDefaultWorldData();
        }

        public void Handle(WordObjectInserted value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (Get(value.TargetPosition).Occupier.HasValue)
                throw  new InvalidOperationException($"Position is already occupied! Position: {value.TargetPosition}");

            Set(value.TargetPosition, value.Target);
        }

        public void Handle(WordObjectDeleted value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            Set(value.TargetPosition,null);
        }

        public void Handle(WordObjectRepositioned value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if ( Get(value.TargetPosition).Occupier.HasValue && Get(value.TargetPosition).Occupier.Value()!=value.Target)
                throw new InvalidOperationException($"Position is already occupied! Position: {value.TargetPosition}");

            Set(value.OldPosition,null);
            Set(value.TargetPosition, value.Target);
        }

        public WorldData Get(WordPosition position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            if (!position.IsValidInWord(_wordSize))
                throw new ArgumentException("Position is not valid in current world. It exceeds world size.",nameof(position));

            return _wordData[position.X,position.Y];
        }
    }
}