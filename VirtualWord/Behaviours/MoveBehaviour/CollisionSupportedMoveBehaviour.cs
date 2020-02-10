using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VirtualWord.Utils;
using VirtualWord.WordObjects;
using VirtualWord.WordObjects.CollisionHandler;
using VirtualWord.WordObjects.CollisionHandler.Base;
using VirtualWord.World;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class CollisionSupportedMoveBehaviour : IMoveBehaviour
    {
        private readonly Movable _movingObject;
        private readonly IMoveBehaviour _moveBehaviour;
        private readonly IWorldDataProvider _worldDataProvider;
        private readonly ICollisionHandler _collisionHandler;

        public CollisionSupportedMoveBehaviour(Movable movingObject,IMoveBehaviour moveBehaviour,
            IWorldDataProvider worldDataProvider,ICollisionHandler collisionHandler)
        {
            if (movingObject == null) throw new ArgumentNullException(nameof(movingObject));
            if (moveBehaviour == null) throw new ArgumentNullException(nameof(moveBehaviour));
            if (worldDataProvider == null) throw new ArgumentNullException(nameof(worldDataProvider));
            if (collisionHandler == null) throw new ArgumentNullException(nameof(collisionHandler));

            _movingObject = movingObject;
            _moveBehaviour = moveBehaviour;
            _worldDataProvider = worldDataProvider;
            _collisionHandler = collisionHandler;
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));

            WordPosition newPosition = _moveBehaviour.GetNewPosition(position);
            Maybe<WordObject> collidingObject = GetCollidingObject(newPosition);

            if (!collidingObject.HasValue)
                return newPosition;
            
            Debug.WriteLine("Collision! " + newPosition);
            _collisionHandler.OnCollision(_movingObject, collidingObject.Value());
            return position;
        }

        private Maybe<WordObject> GetCollidingObject(WordPosition positionToCheck)
        {
            //return _worldDataProvider.GetCurrentWordObjects().Where(x=>!x.Equals(_movingObject))
            //.SingleOrDefault(x => x.Position.Equals(positionToCheck));
            return _worldDataProvider.Get(positionToCheck).Occupier;
        }
    }
}