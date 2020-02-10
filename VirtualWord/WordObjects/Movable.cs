using System;
using VirtualWord.Behaviours.DeathBehaviour;
using VirtualWord.Behaviours.MoveBehaviour;
using VirtualWord.Behaviours.RotateBehaviour;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.WordObjects.Properties.Strenght;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Events;

namespace VirtualWord.WordObjects
{
    public class Movable : WordObject, IRotatable
    {
        private readonly Utils.IObserver<WordObjectRepositioned> _repositionObserver;
        private IRotationBehaviour _rotationBehaviour;
        public IRotationBehaviour RotationBehaviour
        {
            get { return _rotationBehaviour; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _rotationBehaviour = value;
            }
        }

        private IMoveBehaviour _moveBehaviour;
        public IMoveBehaviour MoveBehaviour
        {
            get { return _moveBehaviour; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _moveBehaviour = value;
            }
        }
       
        public double DirectionAngle { get; private set; }
        
        #region Constructors

        public Movable(WordPosition position, Utils.IObserver<WordObjectRepositioned> repositionObserver, IMoveBehaviour moveBehaviour, 
            IRotationBehaviour rotationBehaviour, IDeathBehaviour deathBehaviour, IWorldObjectStrenghtProvider strenghtProvider)
            : base(deathBehaviour, strenghtProvider)
        {
            
            if (position == null) throw new ArgumentNullException(nameof(position));
            if (moveBehaviour == null) throw new ArgumentNullException(nameof(moveBehaviour));
            if (rotationBehaviour == null) throw new ArgumentNullException(nameof(rotationBehaviour));
            if (repositionObserver == null) throw new ArgumentNullException(nameof(repositionObserver));

            Position = position;
            _moveBehaviour = moveBehaviour;
            _rotationBehaviour = rotationBehaviour;
            _repositionObserver = repositionObserver;
        }

        public Movable(WordPosition position,Utils.IObserver<WordObjectRepositioned> repositionObserver ,int strenght = 0 ) 
            : this(position,repositionObserver,new DoNotMove(), new DoNotRotate(), new NeverDie(), new ConstantStrenght(strenght))
        {           
        }

        #endregion

        protected override void TickSpecific()
        {    
            var oldPosition = new WordPosition(Position);

            Position = MoveBehaviour.GetNewPosition(Position);
            DirectionAngle = RotationBehaviour.GetNewRotationAngle(DirectionAngle);

            OnRepositioned(oldPosition);
        }

        private void OnRepositioned(WordPosition oldPosition)
        {
            if (!Equals(oldPosition, Position))
                _repositionObserver.Handle(new WordObjectRepositioned(this, oldPosition));
        }
    }
}