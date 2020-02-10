using System;
using VirtualWord.WordObjects;

namespace WordSimConsole
{
    static class ConsoleWorldObjectRepresentation
    {
        public static string Get(WordObject wo)
        {
            dynamic w = wo;
            return GetSpecific(w);
        }

        private static string GetSpecific(Movable m)
        {
            double angle = m.DirectionAngle;
            if (angle > 0 && angle <= Math.PI / 2)
                return "^";
            if (angle > Math.PI / 2 && angle <= Math.PI)
                return ">";
            if (angle > Math.PI && angle <= Math.PI*1.5)
                return "v";
            return "<";
        }
        private static string GetSpecific(Plant p)
        {
            return "*";
        }

    }
}