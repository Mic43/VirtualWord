using System.Collections.Generic;
using VirtualWord.World;
using VirtualWord.World.Interfaces;

namespace VirtualWord.Behaviours.MoveBehaviour
{
    public class MoveToNearestWorldObjectOfType<TObjectType> : MoveToNearestWorldObject
    { 
        public MoveToNearestWorldObjectOfType(
            IWordObjectsProvider wordObjectsProvider,
            IEnumerable<IMoveBehaviour> moveBehaviours,
            IMoveBehaviour whenNothingToChase)
            : base ( wordObjectsProvider, 
                moveBehaviours,whenNothingToChase,x=> x is TObjectType)
        {

        }
    }
}