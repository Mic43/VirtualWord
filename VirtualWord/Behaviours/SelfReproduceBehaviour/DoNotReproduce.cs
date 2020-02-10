using System.Collections.Generic;
using System.Linq;
using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.SelfReproduceBehaviour
{
    class DoNotReproduce : ISelfReproduceBehaviour
    {
        public IReadOnlyCollection<WordObject> Reproduce(WordObject source)
        {
            return Enumerable.Empty<WordObject>().ToList();
        }
    }
}