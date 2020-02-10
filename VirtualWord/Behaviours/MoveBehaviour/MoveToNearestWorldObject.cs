using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.Utils;
using VirtualWord.WordObjects;
using VirtualWord.World;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    /// <summary>
    /// Chooses move that is nearest to the nearest other world object
    /// </summary>
    public class MoveToNearestWorldObject : IMoveBehaviour
    {
        private readonly IWordObjectsProvider _wordObjectsProvider;
        private readonly IEnumerable<IMoveBehaviour> _moveBehaviours;
        private readonly IMoveBehaviour _whenNothingToChase;
        private readonly Func<WordObject, bool> _includeInSearchFunc;

        public MoveToNearestWorldObject(IWordObjectsProvider wordObjectsProvider, 
            IEnumerable<IMoveBehaviour> moveBehaviours,
            IMoveBehaviour whenNothingToChase,
            Func<WordObject,bool> includeInSearchFunc)
        {
            if (wordObjectsProvider == null) throw new ArgumentNullException(nameof(wordObjectsProvider));
            if (moveBehaviours == null) throw new ArgumentNullException(nameof(moveBehaviours));
            if (whenNothingToChase == null) throw new ArgumentNullException(nameof(whenNothingToChase));
            if (includeInSearchFunc == null) throw new ArgumentNullException(nameof(includeInSearchFunc));

            _wordObjectsProvider = wordObjectsProvider;
            _moveBehaviours = moveBehaviours;
            _whenNothingToChase = whenNothingToChase;
            _includeInSearchFunc = includeInSearchFunc;
        }
        public MoveToNearestWorldObject(IWordObjectsProvider wordObjectsProvider,
          IEnumerable<IMoveBehaviour> moveBehaviours,
          IMoveBehaviour whenNothingToChase ) : this(wordObjectsProvider, moveBehaviours, whenNothingToChase,(o => true))        
        {        
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            var nearest = WorldObjectsHelper.GetNearestWorldObjectTo(position,
                _wordObjectsProvider.GetCurrentWordObjects()
                    .Where(x => _includeInSearchFunc(x)));
               

            if (!nearest.HasValue)
                return _whenNothingToChase.GetNewPosition(position);

            var newPositions = _moveBehaviours.Select(x=> new {NewPosition = x.GetNewPosition(position)}).ToList();
            return
                newPositions.Select(
                    x =>
                        new
                        {
                            NewPosition = x.NewPosition,
                            Distance = nearest.Value().Position.DistanceTo(x.NewPosition)
                        })
                    .OrderBy(x => x.Distance).First().NewPosition;
        }
    }
}