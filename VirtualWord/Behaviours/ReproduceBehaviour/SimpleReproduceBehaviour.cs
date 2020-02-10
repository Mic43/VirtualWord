using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Autofac;
using VirtualWord.Behaviours.MoveBehaviour;
using VirtualWord.Utils;
using VirtualWord.Utils.Factories;
using VirtualWord.WordObjects;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Behaviours.ReproduceBehaviour
{
    public class SimpleReproduceBehaviour : IReproduceBehaviour
    {
        private readonly IWordObjectsFactory _objectsFactory;
        private readonly int _numberOfChildren;
        private readonly IEnumerable<IMoveBehaviour> _availableInitialChildrenMoves;

        public SimpleReproduceBehaviour(IWordObjectsFactory objectsFactory, 
             IEnumerable<IMoveBehaviour> availableInitialChildrenMoves, int numberOfChildren = 1)
        {
            if (objectsFactory == null) throw new ArgumentNullException(nameof(objectsFactory));
            if (availableInitialChildrenMoves == null) throw new ArgumentNullException(nameof(availableInitialChildrenMoves));

            _objectsFactory = objectsFactory;
            _availableInitialChildrenMoves = availableInitialChildrenMoves;
            _numberOfChildren = numberOfChildren;
        }

        public IReadOnlyCollection<WordObject> Reproduce(WordObject object1, WordObject object2)
        {
            if (object1 == null) throw new ArgumentNullException(nameof(object1));
            if (object2 == null) throw new ArgumentNullException(nameof(object2));
          
            if (object1.GetType() != object2.GetType())
                throw  new ArgumentException("Runtime type of both parametrs must be the same to reproduce!");
  
            Debug.WriteLine("Reproducing ");


            // possible refactor to another dependecny, or decorate it
            if (object1.Age < 50 || object2.Age < 50)
                return Enumerable.Empty<WordObject>().ToList();

            var emptyNeigbours = GetEmptyNeigbours(object1.Position);
          
            var actualChildrenCount = Math.Min(_numberOfChildren, emptyNeigbours.Count);
            return Enumerable.Range(0,actualChildrenCount).ToList()
                .Select(number => _objectsFactory.Create(object1.GetType(), emptyNeigbours[number]))
                .ToList();
        }
        
        private IList<WordPosition> GetEmptyNeigbours(WordPosition neigboursFrom)
        {         

            return _availableInitialChildrenMoves.Select(x => x.GetNewPosition(neigboursFrom))
                                                 .Where(position => !position.Equals(neigboursFrom))
                                                 .ToList();
        }
    }
}