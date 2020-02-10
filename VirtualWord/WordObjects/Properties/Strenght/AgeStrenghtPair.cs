using System;

namespace VirtualWord.WordObjects.Properties.Strenght
{
    struct AgeStrenghtPair
    {
        public int Age { get; private set; }
        public int Strenght { get; private set; }

        public AgeStrenghtPair(int age, int strenght)
        {
            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age));
            if (strenght < 0)
                throw new ArgumentOutOfRangeException(nameof(strenght));

            Age = age;
            Strenght = strenght;
        }

    }
}