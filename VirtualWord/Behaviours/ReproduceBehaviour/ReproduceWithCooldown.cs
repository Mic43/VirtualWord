using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.ReproduceBehaviour
{
    public class ReproduceWithCooldown : IReproduceBehaviour
    {
        private readonly IReproduceBehaviour _reproduceBehaviour;        

        public ReproduceWithCooldown(IReproduceBehaviour reproduceBehaviour)
        {
            if (reproduceBehaviour == null) throw new ArgumentNullException(nameof(reproduceBehaviour));
            _reproduceBehaviour = reproduceBehaviour;            
        }

        public IReadOnlyCollection<WordObject> Reproduce(WordObject object1, WordObject object2)
        {
            if (CannotReproduce(object1, object2))
                return Enumerable.Empty<WordObject>().ToList();

            var children = _reproduceBehaviour.Reproduce(object1, object2);
            if (children.Any())
            {
                object1.SetJustReproduced();
                object2.SetJustReproduced();
            }
            return children;
        }

        private bool CannotReproduce(WordObject object1, WordObject object2)
        {
            return !object1.CanReproduceNow || !object2.CanReproduceNow;
        }
    }
}