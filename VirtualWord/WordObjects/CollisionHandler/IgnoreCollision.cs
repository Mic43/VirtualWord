using VirtualWord.WordObjects.CollisionHandler.Base;
using VirtualWord.WordObjects.Interfaces;

namespace VirtualWord.WordObjects.CollisionHandler
{
    public class IgnoreCollision : ICollisionHandler
    {
        public void OnCollision(Movable wantToMoveHere, IPositionable wasHere)
        {         
        }
    }
}