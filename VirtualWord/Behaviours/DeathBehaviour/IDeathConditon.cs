using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.DeathBehaviour
{
    public interface IDeathConditon
    {
        bool IsMetFor(WordObject wordObject);
    }
}