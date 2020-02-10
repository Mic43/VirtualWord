using VirtualWord.WordObjects;
using VirtualWord.WordObjects.Interfaces;

namespace VirtualWord.Behaviours.FightBehaviour
{
    public interface IFightBehaviour
    {
        void Fight(Movable attacker, WordObject defender);
    }
}