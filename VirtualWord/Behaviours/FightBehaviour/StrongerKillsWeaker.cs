using System;
using VirtualWord.WordObjects;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.WordObjectsUpdater;

namespace VirtualWord.Behaviours.FightBehaviour
{
    public class StrongerKillsWeaker : IFightBehaviour
    {
        private readonly IWordObjectsDeleter _deleter;

        public StrongerKillsWeaker(IWordObjectsDeleter deleter)
        {
            if (deleter == null) throw new ArgumentNullException(nameof(deleter));
            _deleter = deleter;
        }

        public void Fight(Movable attacker, WordObject defender)
        {
            var objToRemove =  attacker.Strenght >= defender.Strenght ? defender: attacker;
            _deleter.Delete(objToRemove);
        }
    }
}