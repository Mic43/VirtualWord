using System;
using System.Diagnostics;
using VirtualWord.Behaviours.FightBehaviour;
using VirtualWord.Behaviours.ReproduceBehaviour;
using VirtualWord.WordObjects.CollisionHandler.Base;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.WordObjectsUpdater;

namespace VirtualWord.WordObjects.CollisionHandler
{
    /// <summary>
    /// initialized when no specific collision handler has been found
    /// </summary>
    /// <typeparam name="TMovable1"></typeparam>
    /// <typeparam name="TWordObject"></typeparam>
    public class MovableCollisionBase<TMovable1,TWordObject> : ICollisionHandler<TMovable1, TWordObject> 
        where TMovable1:Movable 
        where TWordObject:WordObject
    {
        private readonly IWordObjectsInserter _wordObjectsInserter;
        private readonly IReproduceBehaviour _reproduceBehaviour;
        private readonly IFightBehaviour _fightBehaviour;

        public MovableCollisionBase(IWordObjectsInserter wordObjectsInserter,
            IReproduceBehaviour reproduceBehaviour, IFightBehaviour fightBehaviour)
        {
            if (wordObjectsInserter == null) throw new ArgumentNullException(nameof(wordObjectsInserter));
            if (reproduceBehaviour == null) throw new ArgumentNullException(nameof(reproduceBehaviour));
            if (fightBehaviour == null) throw new ArgumentNullException(nameof(fightBehaviour));

            _wordObjectsInserter = wordObjectsInserter;
            _reproduceBehaviour = reproduceBehaviour;
            _fightBehaviour = fightBehaviour;
        }

        public void OnCollision(TMovable1 wantToMoveHere, TWordObject wasHere)
        {
            if (wantToMoveHere == null) throw new ArgumentNullException(nameof(wantToMoveHere));
            if (wasHere == null) throw new ArgumentNullException(nameof(wasHere));

            if (!AreMovablesOfTheSameType(wantToMoveHere, wasHere))
            {
                _fightBehaviour.Fight(wantToMoveHere, wasHere);
                return;
            }
            //Debug.WriteLine("Reproducing...");
            var children = _reproduceBehaviour.Reproduce(wantToMoveHere, wasHere);
            foreach (var wordObject in children)
            {
                _wordObjectsInserter.Insert(wordObject);
            }           
        }

        private bool AreMovablesOfTheSameType(TMovable1 wantToMoveHere, TWordObject wasHere)
        {
            return wantToMoveHere.GetType() == wasHere.GetType();
        }
    }
}