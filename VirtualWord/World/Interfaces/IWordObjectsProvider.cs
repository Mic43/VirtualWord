using System.Collections.Generic;
using VirtualWord.WordObjects;

namespace VirtualWord.World.Interfaces
{
    public interface IWordObjectsProvider
    {
        IEnumerable<WordObject> GetCurrentWordObjects();
    }
}