using System;

namespace VirtualWord.WordObjects.Properties.Strenght
{
    public class ConstantStrenght : IWorldObjectStrenghtProvider
    {
        public int Strenght { get; private set; }

        public ConstantStrenght(int strenght)
        {
            if (strenght < 0 )
                throw new ArgumentOutOfRangeException(nameof(strenght),"Strenght must be grater ro equal 0");
            Strenght = strenght;
        }

        public int Get()
        {
            return Strenght;
        }
    }
}