using System;
using VirtualWord.Utils;
using VirtualWord.WordObjects;
using VirtualWord.World;
using VirtualWord.World.DataTypes;

namespace VirtualWord
{
    public interface IWordObjectsFactory
    {        
        WordObject Create(Type type,WordPosition position);
    }
}