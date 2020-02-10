using System;

namespace VirtualWord.Behaviours.SelfReproduceBehaviour.Conditions
{
    public class RandomReproduceConditon : IReproduceConditon
    {
        Random r = new Random();
        public RandomReproduceConditon()
        {
            
        }
        public bool IsMet()
        {
            return r.Next(40) == 0;
        }
    }
}