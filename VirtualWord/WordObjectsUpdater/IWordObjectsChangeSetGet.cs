using System;
using System.Collections.Generic;
using VirtualWord.Utils;
using VirtualWord.WordObjects;
using VirtualWord.World;

namespace VirtualWord.WordObjectsUpdater
{
    public interface IWordObjectsChangeSetGet
    {
        IReadOnlyCollection<Tuple<ChangeSet<WordObject>.Operation,WordObject>> Changes { get;}
    }
}