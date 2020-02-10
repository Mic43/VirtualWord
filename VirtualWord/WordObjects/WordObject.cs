using System;
using VirtualWord.Behaviours.DeathBehaviour;
using VirtualWord.Utils;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.WordObjects.Properties.Strenght;
using VirtualWord.WordObjectsUpdater;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.WordObjects
{
    public abstract class WordObject : ITickReceiver, IPositionable, IReproducer
    {
        public int Age { get; private set; }
        public WordPosition Position { get; protected set; }

        public int Strenght
        {
            get { return _strenghtProvider.Get(); }
        }

        private IWorldObjectStrenghtProvider _strenghtProvider;
        public IWorldObjectStrenghtProvider StrenghtProvider
        {
            get { return _strenghtProvider; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _strenghtProvider = value;
            }
        }

        private IDeathBehaviour _deathBehaviour;
        public IDeathBehaviour DeathBehaviour
        {
            get { return _deathBehaviour; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _deathBehaviour = value;
            }
        }

        #region To Be Removed

        private int _replicationCooldown = 50;

        private int _lastReplicationAge = 0;

        public bool CanReproduceNow
        {
            get { return (Age > _lastReplicationAge + _replicationCooldown); }

        }

        public void SetJustReproduced()
        {
            _lastReplicationAge = Age;
        }

        #endregion

        protected WordObject(IDeathBehaviour deathBehaviour, IWorldObjectStrenghtProvider strenghtProvider)
        {
            if (deathBehaviour == null) throw new ArgumentNullException(nameof(deathBehaviour));
            if (strenghtProvider == null) throw new ArgumentNullException(nameof(strenghtProvider));
            _strenghtProvider = strenghtProvider;
            DeathBehaviour = deathBehaviour;
        }

        protected abstract void TickSpecific();

        public void OnTick()
        {
            TickSpecific();
            Age++;
            DeathBehaviour.TryDie(this);
        }
    }
}