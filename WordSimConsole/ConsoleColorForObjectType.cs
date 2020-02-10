using System;
using VirtualWord.WordObjects;

namespace WordSimConsole
{
    class ConsoleColorForObjectType
    {
        public ConsoleColor Get(WordObject wo)
        {
            dynamic w = wo;
            return GetSpecific(w);
        }

        private ConsoleColor GetSpecific(Movable m)
        {
            return ConsoleColor.Gray;            
        }
        private ConsoleColor GetSpecific(ConcreteAnimalSample m)
        {
            return ConsoleColor.Yellow;
        }
        private ConsoleColor GetSpecific(ConcreteAnimalSample2 m)
        {
            return ConsoleColor.Blue;
        }
        private ConsoleColor GetSpecific(Plant m)
        {
            return ConsoleColor.Green;
        }


    }
}