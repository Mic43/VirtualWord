using System;
using Autofac;
using VirtualWord.WordObjects.Interfaces;

namespace VirtualWord.WordObjects.CollisionHandler.Base
{
    /// <summary>
    /// Uses proper  ICollisionHandler<T,V> basing on runtime types of OnCollision parameters </T>
    /// </summary>
    public class HandleForConcreteWordObjectsTypes : ICollisionHandler
    {
        private readonly ILifetimeScope _scope;

        public HandleForConcreteWordObjectsTypes(ILifetimeScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            _scope = scope;
        }

        public void OnCollision(Movable wantToMoveHere, IPositionable wasHere)
        {
            if (wantToMoveHere == null) throw new ArgumentNullException(nameof(wantToMoveHere));
            if (wasHere == null) throw new ArgumentNullException(nameof(wasHere));

            var type = typeof(ICollisionHandler<,>).MakeGenericType(wantToMoveHere.GetType(),wasHere.GetType());

            object handler = _scope.Resolve(type);
            dynamic handlerDynamic = handler;

            handlerDynamic.OnCollision((dynamic)wantToMoveHere, (dynamic) wasHere);
        }
    }
      
}