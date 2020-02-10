using System;
using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.DeathBehaviour
{
    public class DieAtConstantAge : IDeathConditon
    {
        public int Age { get; private set; }

        public DieAtConstantAge(int age)
        {
            if(age < 0 )
                throw  new ArgumentOutOfRangeException(nameof(age));
            Age = age;
        }

        public bool IsMetFor(WordObject wordObject)
        {
            return wordObject.Age >= Age;
        }
    }
}