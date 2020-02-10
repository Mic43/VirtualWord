using System.Collections.Generic;
using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.ReproduceBehaviour
{
    public interface IReproduceBehaviour
    {
        IReadOnlyCollection<WordObject> Reproduce(WordObject object1, WordObject object2);
    }
}