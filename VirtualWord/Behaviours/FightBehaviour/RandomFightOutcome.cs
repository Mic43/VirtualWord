using System;
using VirtualWord.WordObjects;
using VirtualWord.WordObjects.Interfaces;
using VirtualWord.WordObjectsUpdater;

namespace VirtualWord.Behaviours.FightBehaviour
{
    public class RandomFightOutcome : IFightBehaviour
    {
        private readonly IWordObjectsDeleter _deleter;
        private readonly Random _random;

        public RandomFightOutcome(IWordObjectsDeleter deleter,Random random)
        {
            if (deleter == null) throw new ArgumentNullException(nameof(deleter));
            if (random == null) throw new ArgumentNullException(nameof(random));
            _deleter = deleter;
            _random = random;
        }

        public void Fight(Movable attacker, WordObject defender)
        {
            var rnd = _random.Next(2);
            _deleter.Delete(rnd == 0 ? attacker : defender);
        }
    }
}