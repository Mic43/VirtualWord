using System;
using System.Collections.Generic;
using Autofac;
using VirtualWord.Behaviours.DeathBehaviour;
using VirtualWord.Behaviours.MoveBehaviour;
using VirtualWord.Behaviours.SelfReproduceBehaviour;
using VirtualWord.WordObjects.Properties.Strenght;
using VirtualWord.WordObjectsUpdater;
using VirtualWord.World.DataTypes;

namespace VirtualWord.WordObjects
{
    public abstract class Plant : WordObject
    {
        private ISelfReproduceBehaviour _selfReproduceBehaviour;
        private readonly IWordObjectsInserter _inserter;

        protected Plant(WordPosition position,IWordObjectsInserter inserter) :
                this(position, new DoNotReproduce(), new NeverDie(), inserter, 0)
        {
            
        }

        protected Plant(WordPosition position,ISelfReproduceBehaviour selfReproduceBehaviour, 
            IDeathBehaviour deathBehaviour, IWordObjectsInserter inserter,
            int strenght)
            : base(deathBehaviour, new ConstantStrenght(strenght))
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            if (selfReproduceBehaviour == null) throw new ArgumentNullException(nameof(selfReproduceBehaviour));
            if (inserter == null) throw new ArgumentNullException(nameof(inserter));

            _selfReproduceBehaviour = selfReproduceBehaviour;
            _inserter = inserter;
            Position = position;
        }

        public ISelfReproduceBehaviour SelfReproduceBehaviour
        {
            get { return _selfReproduceBehaviour; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _selfReproduceBehaviour = value;
            }
        }

        protected override void TickSpecific()
        {            
            var children = _selfReproduceBehaviour.Reproduce(this);
            foreach (var child in children)
            {
                _inserter.Insert(child);
            }
        }
    }
}