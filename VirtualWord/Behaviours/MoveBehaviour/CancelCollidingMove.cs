using System;
using System.Linq;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class CancelCollidingMove : IMoveBehaviour
    {
        private readonly IWorldDataProvider _wordDataProvider;
        public IMoveBehaviour MoveBehaviour { get; private set; }

        public CancelCollidingMove(IMoveBehaviour moveBehaviour,IWorldDataProvider wordDataProvider)
        {
            if (moveBehaviour == null) throw new ArgumentNullException(nameof(moveBehaviour));
            if (wordDataProvider == null) throw new ArgumentNullException(nameof(wordDataProvider));

            _wordDataProvider = wordDataProvider;
            MoveBehaviour = moveBehaviour;
        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            var newWordPosition = MoveBehaviour.GetNewPosition(position);
            return _wordDataProvider.Get(newWordPosition).Occupier.HasValue ? position : newWordPosition;
        }
    }
}