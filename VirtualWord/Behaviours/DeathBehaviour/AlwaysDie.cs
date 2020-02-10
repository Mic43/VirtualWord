using System;
using System.Diagnostics;
using VirtualWord.WordObjects;
using VirtualWord.WordObjectsUpdater;

namespace VirtualWord.Behaviours.DeathBehaviour
{
    public class Die : IDeathBehaviour
    {
        public IWordObjectsDeleter WordObjectsDeleter { get; private set; }

        public Die(IWordObjectsDeleter wordObjectsDeleter)
        {
            if (wordObjectsDeleter == null) throw new ArgumentNullException(nameof(wordObjectsDeleter));
            WordObjectsDeleter = wordObjectsDeleter;
        }

        public void TryDie(WordObject wordObject)
        {
            Debug.WriteLine("Death");
            WordObjectsDeleter.Delete(wordObject);
        }
    }
}