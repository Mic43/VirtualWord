using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.DeathBehaviour
{
    public interface IDeathBehaviour
    {
        void TryDie(WordObject wordObject);
    }
}