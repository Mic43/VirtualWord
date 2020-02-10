using VirtualWord.WordObjects;
using VirtualWord.WordObjects.Interfaces;

namespace VirtualWord.Behaviours.FightBehaviour
{
    public class DoNotAttack : IFightBehaviour
    {
        public void Fight(Movable attacker, WordObject defender)
        {
        }
    }
}