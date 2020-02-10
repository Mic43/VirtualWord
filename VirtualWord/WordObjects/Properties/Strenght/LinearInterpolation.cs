using System;
using System.Collections.Generic;

namespace VirtualWord.WordObjects.Properties.Strenght
{
    class LinearInterpolation : IAgeToStrenghtFunc
    {
        private readonly List<AgeStrenghtPair> _ageStrenghtPairs;

        public LinearInterpolation(IEnumerable<AgeStrenghtPair> ageStrenghtPairs )
        {
            if (ageStrenghtPairs == null) throw new ArgumentNullException(nameof(ageStrenghtPairs));
            _ageStrenghtPairs = new List<AgeStrenghtPair>(ageStrenghtPairs);
            if(_ageStrenghtPairs.Count < 2)
                throw new ArgumentException("Size of the ageStrenghtPairs must be more than 1",nameof(ageStrenghtPairs));

            _ageStrenghtPairs.Sort(new ByAgeComparer());
        }

        internal class ByAgeComparer : IComparer<AgeStrenghtPair>
        {
            public int Compare(AgeStrenghtPair x, AgeStrenghtPair y)
            {
                return x.Age.CompareTo(y.Age);
            }
        }

        public int Calculate(int age)
        {
            int i = 0;
            while (i < _ageStrenghtPairs.Count && age > _ageStrenghtPairs[i].Age)
            {
                i++;
            }

            if (i == 0) // if we are lest htan minimal age we should take next available points
                i++;
            else if (i == _ageStrenghtPairs.Count)
                i--;

            int x1 = _ageStrenghtPairs[i - 1].Age;
            int y1 = _ageStrenghtPairs[i - 1].Strenght;

            int x2 = _ageStrenghtPairs[i].Age;
            int y2 = _ageStrenghtPairs[i].Strenght;


            double a = (double) (y2 - y1)/(x2 - x1);
            double b = -x1*a + y1;
          
            return ((int) (a*age + b));

        }
    }
}