using System;
using Autofac;
using VirtualWord.Behaviours.ReproduceBehaviour;
using VirtualWord.Utils;
using VirtualWord.WordObjects;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord
{
    public class AutofacWordObjectsFactory : IWordObjectsFactory
    {
        private readonly ILifetimeScope _scope;

        public AutofacWordObjectsFactory(ILifetimeScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            _scope = scope;
        }

        public WordObject Create(Type type, WordPosition position)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!type.IsAssignableTo<WordObject>())
                throw new ArgumentException("Provided type must derive from WordObject class", nameof(type));

            return (WordObject) _scope.Resolve(type,new NamedParameter("position",position));
        }
    }
}