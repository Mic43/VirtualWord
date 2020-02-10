using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.Utils;
using VirtualWord.WordObjects;
using VirtualWord.World;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class SelectMoveBasingOnSpecificObject : IMoveBehaviour
    {
        private readonly Func<IEnumerable<WordObject>, WordObject> _chooseObjectFunc;
        private readonly IWordObjectsProvider _wordObjectsProvider;
        private readonly IEnumerable<IMoveBehaviour> _behavioursToChooseFrom;
        private readonly Func<IEnumerable<IMoveBehaviour>, WordObject, IMoveBehaviour> _behaviourChooseFunc;

        public SelectMoveBasingOnSpecificObject(IWordObjectsProvider wordObjectsProvider,
            Func<IEnumerable<WordObject>,WordObject>  chooseObjectFunc,
            IEnumerable<IMoveBehaviour> behavioursToChooseFrom,
            Func<IEnumerable<IMoveBehaviour>,WordObject,IMoveBehaviour> behaviourChooseFunc   )
        {
            if (chooseObjectFunc == null) throw new ArgumentNullException(nameof(chooseObjectFunc));
            if (behaviourChooseFunc == null) throw new ArgumentNullException(nameof(behaviourChooseFunc));
            if (wordObjectsProvider == null) throw new ArgumentNullException(nameof(wordObjectsProvider));
            if (behavioursToChooseFrom == null) throw new ArgumentNullException(nameof(behavioursToChooseFrom));

            _chooseObjectFunc = chooseObjectFunc;
            _behaviourChooseFunc = behaviourChooseFunc;
            _wordObjectsProvider = wordObjectsProvider;
            _behavioursToChooseFrom = behavioursToChooseFrom;
        }

//        public SelectMoveBasingOnSpecificObject()
//        {
//            Type chaserType = typeof(Movable);
//            WordObject sourceObject = null;
//            WordPosition movablePos = new WordPosition();
//
//            var behavs = _behavioursToChooseFrom.Select(x => x.GetNewPosition(movablePos))
//                    .Select(availableNewPosition =>
//                        new
//                        {
//                            NewPosition = availableNewPosition,
//                            Distance = sourceObject.Position.DistanceTo(availableNewPosition)
//                        })
//                    .OrderBy(x => x.Distance);
//
//            Func<IEnumerable<WordObject>, WordObject> chooseObjectToChaseFunc = objects =>
//            {
//                var list = objects.Where(x => IsInFieldOfView(x)).ToLookup(x=> x.GetType() == chaserType);
//                
//                var notEmptyList = list[false].Any() ? list[false] : list[true];
//                //unsafe
//                return WorldObjectsHelper.GetNearestWorldObjectTo(sourceObject.Position, notEmptyList).Value();
//
//            };
//        }

        public WordPosition GetNewPosition(WordPosition position)
        {
            return
                _behaviourChooseFunc(_behavioursToChooseFrom,
                    _chooseObjectFunc(_wordObjectsProvider.GetCurrentWordObjects())).GetNewPosition(position);
            //    _behavioursToChoose.

//            return _behavioursToChoose.Select(x => x.GetNewPosition(position))
//                .Select( availableNewPositions =>
//                    new
//                    {
//                        NewPosition = availableNewPositions,
//                        Distance = objectToChase.Position.DistanceTo(availableNewPositions)
//                    })
//                .OrderBy(x => x.Distance).First().NewPosition;
        }
    }
}