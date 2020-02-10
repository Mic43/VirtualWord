using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.WordObjects;
using VirtualWord.WordObjectsUpdater;
using VirtualWord.World.Events;
using VirtualWord.World.Interfaces;

namespace VirtualWord.World
{
    public class WordObjectsContainer : IWordObjectsProvider,IWordObjectsInserter,IWordObjectsDeleter
    {
        private readonly IList<WordObject> _wordObjects;
        private readonly Utils.IObserver<WordObjectInserted> _insertObserver;
        private readonly Utils.IObserver<WordObjectDeleted> _deleteObserver;

        public WordObjectsContainer(Utils.IObserver<WordObjectInserted> insertObserver,Utils.IObserver<WordObjectDeleted> deleteObserver)
            : this(new List<WordObject>(), insertObserver, deleteObserver)
        {
        }

        public WordObjectsContainer(IList<WordObject> wordObjects,
            Utils.IObserver<WordObjectInserted> insertObserver, 
            Utils.IObserver<WordObjectDeleted> deleteObserver)
        {
            if (wordObjects == null) throw new ArgumentNullException(nameof(wordObjects));
            if (insertObserver == null) throw new ArgumentNullException(nameof(insertObserver));
            if (deleteObserver == null) throw new ArgumentNullException(nameof(deleteObserver));

            _wordObjects = wordObjects;
            _insertObserver = insertObserver;
            _deleteObserver = deleteObserver;
        }

        public IEnumerable<WordObject> GetCurrentWordObjects()
        {
            return _wordObjects;
        }

        public void Insert(WordObject newWordObject)
        {                        
            _wordObjects.Add(newWordObject);
            _insertObserver.Handle(new WordObjectInserted(newWordObject));
        }

        public void Delete(WordObject wordObject)
        {
            _wordObjects.Remove(wordObject);
            _deleteObserver.Handle(new WordObjectDeleted(wordObject));
        }
    }
}