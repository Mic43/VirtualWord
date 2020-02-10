using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.WordObjects;
using VirtualWord.World.DataTypes;

namespace VirtualWord.Utils
{
    public static class WorldObjectsHelper
    {
        public static Maybe<WordObject> GetNearestWorldObjectTo( WordPosition target,IEnumerable<WordObject> allObjects)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (allObjects == null) throw new ArgumentNullException(nameof(allObjects));
            
            return new Maybe<WordObject>(allObjects.Select(x => new {Distance = x.Position.DistanceTo(target), WordObject = x})
                .Where(x => x.Distance != 0)
                .OrderBy(x => x.Distance)
                .Select(x => x.WordObject)
                .FirstOrDefault());
        }
    }
}