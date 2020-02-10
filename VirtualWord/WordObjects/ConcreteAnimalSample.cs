using System;
using System.Linq;
using Autofac;
using VirtualWord.Behaviours;
using VirtualWord.Behaviours.DeathBehaviour;
using VirtualWord.Behaviours.MoveBehaviour;

using VirtualWord.WordObjects.CollisionHandler.Base;
using VirtualWord.WordObjects.Properties.Strenght;
using VirtualWord.WordObjectsUpdater;
using VirtualWord.World;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Events;
using VirtualWord.World.Interfaces;
using static VirtualWord.Utils.Factories.CollisionHandlersFactory;
using static VirtualWord.Utils.Factories.MoveBehavioursFactory;

namespace VirtualWord.WordObjects
{
    public class ConcreteAnimalSample : Movable
    {
        private readonly ILifetimeScope _scope;

        public ConcreteAnimalSample(ILifetimeScope scope, IWordSizeProvider provider, 
            Utils.IObserver<WordObjectRepositioned> repositionObserver) : 
            this( scope, provider, repositionObserver,Word.GetRandomPosition(provider.Size, scope.Resolve<Random>()))
        {

        }
        public ConcreteAnimalSample(ILifetimeScope scope,IWordSizeProvider provider, 
            Utils.IObserver<WordObjectRepositioned> repositionObserver, WordPosition position) 
            : base(position, repositionObserver)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            _scope = scope;

            var beh = new RandomMoveBasingOnDirection(this,
                           GetPossibleCrossMoves(1,provider.Size),_scope.Resolve<Random>());
            //var behaviour = new ObjectMustFaceMoveDirection(beh);

            ICollisionHandler collisionHandler = new SequentialCollisionHandler(Wrap(CreateMovableCollisionBase(scope)));


            var behaviour = new ObjectMustFaceMoveDirection(new RepeatingMove(new CollisionSupportedMoveBehaviour(this, beh,
                _scope.Resolve<IWorldDataProvider>(),collisionHandler), count: 1));
            DeathBehaviour = new ConditionalDeathBehaviour(new DieAtConstantAge(250), new Die(_scope.Resolve<IWordObjectsDeleter>()));
            
            RotationBehaviour = behaviour;            
            MoveBehaviour = behaviour;
            AgeStrenghtPair[] array =
            {
                new AgeStrenghtPair(0, 1),
                new AgeStrenghtPair(100, 15),
                new AgeStrenghtPair(250, 5)

            };
            StrenghtProvider = new StrenghtBasedOnAge(() => Age,new LinearInterpolation(array));
        }

        
    }

    public class ConcreteAnimalSample2 : Movable
    {
        private readonly ILifetimeScope _scope;

        public ConcreteAnimalSample2(ILifetimeScope scope, IWordSizeProvider provider, Utils.IObserver<WordObjectRepositioned> repositionObserver) : 
            this( scope,  provider, repositionObserver,Word.GetRandomPosition(provider.Size, scope.Resolve<Random>()))
        {
            
        }
        public ConcreteAnimalSample2(ILifetimeScope scope, IWordSizeProvider provider, 
            Utils.IObserver<WordObjectRepositioned> repositionObserver,WordPosition position) : base(position, repositionObserver,8)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            if (provider == null) throw new ArgumentNullException(nameof(provider));            
            _scope = scope;

            var baseMoves = GetPossibleUpToDistanceMoves(1,provider.Size).ToList();
            var beh = new MoveToNearestWorldObjectOfType<ConcreteAnimalSample>(_scope.Resolve<IWordObjectsProvider>(),
                            baseMoves,new RandomMoveBasingOnDirection(this, baseMoves,scope.Resolve<Random>()));
            //var beh = new RandomMoveBasingOnDirection(this, baseMoves, _scope.Resolve<Random>());
            var behaviour = new ObjectMustFaceMoveDirection(beh);
            var collisionHandler =  new SequentialCollisionHandler(Wrap(CreateMovableCollisionBase(scope)));

            MoveBehaviour = new CollisionSupportedMoveBehaviour(this, behaviour, _scope.Resolve<IWorldDataProvider>(),
                                collisionHandler);
            DeathBehaviour = new ConditionalDeathBehaviour(new DieAtConstantAge(400), new Die(_scope.Resolve<IWordObjectsDeleter>()));

            RotationBehaviour = behaviour;

            AgeStrenghtPair[] array =
            {
                new AgeStrenghtPair(0,1),
                new AgeStrenghtPair(180, 25),
                new AgeStrenghtPair(400, 3)

            };
            StrenghtProvider = new StrenghtBasedOnAge(() => Age, new LinearInterpolation(array));

        }
    }

}