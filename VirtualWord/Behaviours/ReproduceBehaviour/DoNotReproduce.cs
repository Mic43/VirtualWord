using System.Collections.Generic;
using System.Linq;
using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.ReproduceBehaviour
{
    public class DoNotReproduce : IReproduceBehaviour
    {
        public IReadOnlyCollection<WordObject> Reproduce(WordObject object1, WordObject object2)
        {
            return Enumerable.Empty<WordObject>().ToList();
        }
    }
}