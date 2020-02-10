using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.Behaviours.MoveBehaviour;
using VirtualWord.Utils.Factories;
using VirtualWord.WordObjects;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace VirtualWord.Behaviours.SelfReproduceBehaviour
{
    public class RandomSurroundingSelfReproduce : ISelfReproduceBehaviour
    {
        private readonly IWordObjectsFactory _wordObjectsFactory;
        private readonly IList<IMoveBehaviour> _availableInitialChildrenMoves;
        private readonly Random _random;

        public RandomSurroundingSelfReproduce(IWordObjectsFactory wordObjectsFactory,
            IList<IMoveBehaviour> availableInitialChildrenMoves,Random random)
        {
            if (wordObjectsFactory == null) throw new ArgumentNullException(nameof(wordObjectsFactory));
            if (availableInitialChildrenMoves == null) throw new ArgumentNullException(nameof(availableInitialChildrenMoves));
            if (random == null) throw new ArgumentNullException(nameof(random));

            _wordObjectsFactory = wordObjectsFactory;
            _availableInitialChildrenMoves = availableInitialChildrenMoves;
            _random = random;
        }

        public IReadOnlyCollection<WordObject> Reproduce(WordObject source)
        {
            var properBehaviours = _availableInitialChildrenMoves.Where(x=>!Equals(x.GetNewPosition(source.Position), source.Position))
                                    .ToList();
            if (!properBehaviours.Any())
                return Enumerable.Empty<WordObject>().ToList();

            var newChildPosition = properBehaviours[_random.Next(properBehaviours.Count)]
                                  .GetNewPosition(source.Position);
            return Enumerable.Repeat(_wordObjectsFactory.Create(source.GetType(), newChildPosition), 1).ToList();
        }
    }
}