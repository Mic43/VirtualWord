using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VirtualWord.WordObjects;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace VirtualWord.World
{
    public class Word : ITicker, IWordSizeProvider
    {
        private readonly IWordObjectsProvider _wordObjectsProvider;
        public WordSize Size { get; }

       // private readonly IDictionary<WordObject, WordObjectData> dict = new Dictionary<WordObject, WordObjectData>();

        public Word(WordSize wordSize, IWordObjectsProvider wordObjectsProvider)
        {
            if (wordObjectsProvider == null) throw new ArgumentNullException(nameof(wordObjectsProvider));

            _wordObjectsProvider = wordObjectsProvider;
            Size = wordSize;

        }              
        public void Tick()
        {
            IEnumerable<WordObject> currentWordObjects = _wordObjectsProvider.GetCurrentWordObjects().ToList();
            foreach (var worrldObject in currentWordObjects)
            {
                worrldObject.OnTick();
            }
        }

        public static WordPosition GetRandomPosition(WordSize size, Random random)
        {            
            if (random == null) throw new ArgumentNullException(nameof(random));

            return new WordPosition(random.Next(size.X), random.Next(size.Y));
        }
    }
}

