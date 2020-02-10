using System;
using System.Collections.Generic;
using VirtualWord.Utils;
using VirtualWord.WordObjects;
using VirtualWord.World;

namespace VirtualWord.WordObjectsUpdater
{
    public class WordObjectsChangeChangesUpdater : IWordObjectsChangeSetGet, IWordObjectsDeleter, IWordObjectsInserter, IWordObjectsChangeSetClear
    {
        readonly ChangeSet<WordObject> _changeSet = new ChangeSet<WordObject>();
        public void Delete(WordObject wordObject)
        {
            if (wordObject == null) throw new ArgumentNullException(nameof(wordObject));
            _changeSet.Delete(wordObject);
        }

        public void Insert(WordObject wordObject)
        {
            if (wordObject == null) throw new ArgumentNullException(nameof(wordObject));
            _changeSet.Insert(wordObject);
        }

        public IReadOnlyCollection<Tuple<ChangeSet<WordObject>.Operation, WordObject>> Changes
        {
            get { return _changeSet.GetChanges(); }
        }

        public void Clear()
        {
            _changeSet.Clear();
        }
    }
}