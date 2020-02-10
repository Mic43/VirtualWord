using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VirtualWord.WordObjects.Interfaces;

namespace VirtualWord.WordObjects.CollisionHandler.Base
{
    public class SequentialCollisionHandler : ICollisionHandler
    {
        private readonly IEnumerable<ICollisionHandler> _handlers;

        public SequentialCollisionHandler(IEnumerable<ICollisionHandler> handlers)
        {
            if (handlers == null) throw new ArgumentNullException(nameof(handlers));
            _handlers = handlers;
        }

        public SequentialCollisionHandler(params ICollisionHandler[] handlers) : this (handlers.AsEnumerable())
        {
            
        }

        public void OnCollision(Movable wantToMoveHere, IPositionable wasHere)
        {
            foreach (var collisionHandler in _handlers)
            {
                collisionHandler.OnCollision(wantToMoveHere,wasHere);
            }
        }
    }
}