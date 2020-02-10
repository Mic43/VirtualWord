using System;
using Autofac;
using VirtualWord.Behaviours.FightBehaviour;
using VirtualWord.Behaviours.ReproduceBehaviour;
using VirtualWord.WordObjects;
using VirtualWord.WordObjects.CollisionHandler;
using VirtualWord.WordObjects.CollisionHandler.Base;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.WordObjectsUpdater;

namespace VirtualWord.Utils.Factories
{
    public static class CollisionHandlersFactory
    {
        public static MovableCollisionBase<Movable, WordObject> CreateMovableCollisionBase(ILifetimeScope scope)
        {
            return new MovableCollisionBase<Movable, WordObject>(
                scope.Resolve<IWordObjectsInserter>(), scope.Resolve<IReproduceBehaviour>(), scope.Resolve<IFightBehaviour>());
        }

        public static CollisionHandler<T, V> Wrap<T,V>(ICollisionHandler<T,V> collisionHandler ) where T : Movable 
                                                                                                 where V : IPositionable
        {
            if (collisionHandler == null) throw new ArgumentNullException(nameof(collisionHandler));
            return new CollisionHandler<T, V>(collisionHandler);
        }
    }
}