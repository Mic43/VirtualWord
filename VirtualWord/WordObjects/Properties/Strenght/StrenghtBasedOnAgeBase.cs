using System;

namespace VirtualWord.WordObjects.Properties.Strenght
{
    public abstract  class StrenghtBasedOnAgeBase : IWorldObjectStrenghtProvider
    {
        private readonly Func<int> _ageProviderFunc;
        public Func<int> AgeProviderFunc
        {
            get { return _ageProviderFunc; }
        }

        protected StrenghtBasedOnAgeBase(Func<int> ageProviderFunc )
        {
            if (ageProviderFunc == null) throw new ArgumentNullException(nameof(ageProviderFunc));
            _ageProviderFunc = ageProviderFunc;
        }

        public abstract int Get();        
    }
}