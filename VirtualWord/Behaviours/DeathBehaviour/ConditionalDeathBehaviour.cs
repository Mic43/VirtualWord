using System;
using VirtualWord.WordObjects;
using VirtualWord.WordObjectsUpdater;

namespace VirtualWord.Behaviours.DeathBehaviour
{
    public class ConditionalDeathBehaviour : IDeathBehaviour
    {     
        public IDeathConditon DeathConditon { get; private set; }

        public IDeathBehaviour DeathBehaviour { get; private set; }


        public ConditionalDeathBehaviour(IDeathConditon deathConditon, IDeathBehaviour deathBehaviour)
        {
            if (deathConditon == null) throw new ArgumentNullException(nameof(deathConditon));
            if (deathBehaviour == null) throw new ArgumentNullException(nameof(deathBehaviour));

            DeathConditon = deathConditon;
            DeathBehaviour = deathBehaviour;
        }   

        public void TryDie(WordObject wordObject)
        {
            if (DeathConditon.IsMetFor(wordObject))
                DeathBehaviour.TryDie(wordObject);
        }
    }
}