using System;

namespace VirtualWord.WordObjects.Properties.Strenght
{
    class StrenghtBasedOnAge : StrenghtBasedOnAgeBase
    {
        private readonly IAgeToStrenghtFunc _ageToStrenghtFunc;

        public IAgeToStrenghtFunc AgeToStrenghtFunc
        {
            get { return _ageToStrenghtFunc; }
        }

        public StrenghtBasedOnAge(Func<int> ageProviderFunc,IAgeToStrenghtFunc ageToStrenghtFunc) : base(ageProviderFunc)
         {
             if (ageToStrenghtFunc == null) throw new ArgumentNullException(nameof(ageToStrenghtFunc));
             _ageToStrenghtFunc = ageToStrenghtFunc;
         }

        public override int Get()
         {
             return AgeToStrenghtFunc.Calculate(AgeProviderFunc());
         }
    }
}