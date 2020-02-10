using System;
using System.Linq;
using VirtualWord.Behaviours.DeathBehaviour;
using VirtualWord.Behaviours.SelfReproduceBehaviour;
using VirtualWord.Behaviours.SelfReproduceBehaviour.Conditions;
using VirtualWord.Utils.Factories;
using VirtualWord.WordObjectsUpdater;
using VirtualWord.World;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace VirtualWord.WordObjects
{
    public class SamplePlant : Plant
    {

        public SamplePlant(IWordObjectsInserter inserter,IWordObjectsDeleter objectsDeleter, IWordSizeProvider wordSizeProvider,
            IWorldDataProvider worldDataProvider, IWordObjectsFactory wordObjectsFactory,Random random) :

                this(Word.GetRandomPosition(wordSizeProvider.Size, random), 
                    inserter, objectsDeleter,wordSizeProvider, worldDataProvider, wordObjectsFactory,random)
        {
          
        }

        public SamplePlant(WordPosition position,IWordObjectsInserter inserter, 
            IWordObjectsDeleter objectsDeleter,
            IWordSizeProvider wordSizeProvider, IWorldDataProvider worldDataProvider, 
            IWordObjectsFactory wordObjectsFactory, 
            Random random) :
                base (position, inserter)
        {
            if (objectsDeleter == null) throw new ArgumentNullException(nameof(objectsDeleter));
            if (wordSizeProvider == null) throw new ArgumentNullException(nameof(wordSizeProvider));            
            if (worldDataProvider == null) throw new ArgumentNullException(nameof(worldDataProvider));
            if (wordObjectsFactory == null) throw new ArgumentNullException(nameof(wordObjectsFactory));
           
            var moves =  MoveBehavioursFactory.GetAllMovesUpToDistance(1)
                .PossibleInWord(wordSizeProvider.Size)
                .NonColliding(worldDataProvider)
                .ToList();
            SelfReproduceBehaviour = new ConditionalReproduceBehaviour(new RandomReproduceConditon(), 
                new RandomSurroundingSelfReproduce(wordObjectsFactory, moves,random));
            DeathBehaviour = new ConditionalDeathBehaviour(new DieAtConstantAge(100),new Die(objectsDeleter) );
        }
    }
}