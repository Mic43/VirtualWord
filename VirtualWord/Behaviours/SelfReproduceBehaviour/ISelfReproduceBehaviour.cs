using System.Collections;
using System.Collections.Generic;
using VirtualWord.Behaviours.DeathBehaviour;
using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.SelfReproduceBehaviour
{
    public interface ISelfReproduceBehaviour
    {
        IReadOnlyCollection<WordObject> Reproduce(WordObject source);
    }
}