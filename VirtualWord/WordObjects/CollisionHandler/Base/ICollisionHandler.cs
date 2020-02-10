using VirtualWord.WordObjects.Interfaces;

namespace VirtualWord.WordObjects.CollisionHandler.Base
{
    public interface ICollisionHandler
    {
        void OnCollision(Movable wantToMoveHere, IPositionable wasHere);
    }


    public interface ICollisionHandler<in TMovable, in TPositionable> 
        where TMovable : Movable
        where TPositionable : IPositionable       
    {
        // NAME OF THIS METHOD CANNOT BE CHANGED
        void OnCollision(TMovable wantToMoveHere, TPositionable wasHere);
    }

//    class MyClass
//    {
//        List<ICollisionHandler>  a =  new List<ICollisionHandler>()
//        {
//            new TestCollisionHandler<Movable,Movable>(new MovablesCollisionBase<Movable,Movable>(
//                new WordObjectsChangeChangesUpdater(), new DoNotReproduce(), new DoNotAttack()))
//        };
//
//        public MyClass()
//        {
//            Movable m = new ConcreteAnimalSample();
//            IPositionable n = new ConcreteAnimalSample2();
//            
//            a.ForEach(x=>x.OnCollision(m,n));
//        }
//    }
}