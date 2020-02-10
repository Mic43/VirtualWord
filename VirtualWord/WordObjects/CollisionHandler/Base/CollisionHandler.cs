using System;
using VirtualWord.WordObjects.Interfaces;

namespace VirtualWord.WordObjects.CollisionHandler.Base
{
    public class CollisionHandler<TMovable,TPositionable> : ICollisionHandler where TPositionable : IPositionable 
        where TMovable : Movable
    {
        private readonly ICollisionHandler<TMovable, TPositionable> _adaptee;

        public CollisionHandler(ICollisionHandler<TMovable, TPositionable> adaptee)
        {
            if (adaptee == null) throw new ArgumentNullException(nameof(adaptee));
            _adaptee = adaptee;
        }

        public void OnCollision(Movable wantToMoveHere, IPositionable wasHere)
        {
            if (wantToMoveHere is TMovable && wasHere is TPositionable)
                _adaptee.OnCollision((TMovable) wantToMoveHere,(TPositionable) wasHere);
        }
    }
}