using System;
using System.Diagnostics;
using VirtualWord.WordObjects;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.WordObjectsUpdater;

namespace VirtualWord.Behaviours.FightBehaviour
{
    public class SimpleKillDefender : IFightBehaviour
    {
        private readonly IWordObjectsDeleter _deleter;

        public SimpleKillDefender(IWordObjectsDeleter deleter)
        {
            if (deleter == null) throw new ArgumentNullException(nameof(deleter));
            _deleter = deleter;
        }

        public void Fight(Movable attacker, WordObject defender)
        {
            Debug.WriteLine("Fight");
            _deleter.Delete(defender);
        }
    }
}