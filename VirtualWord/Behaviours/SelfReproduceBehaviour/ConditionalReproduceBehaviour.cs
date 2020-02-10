using System;
using System.Collections.Generic;
using System.Linq;
using VirtualWord.Behaviours.SelfReproduceBehaviour.Conditions;
using VirtualWord.WordObjects;

namespace VirtualWord.Behaviours.SelfReproduceBehaviour
{
    public class ConditionalReproduceBehaviour : ISelfReproduceBehaviour
    {
        public IReproduceConditon ReproduceConditon { get; private set; }

        public ISelfReproduceBehaviour SelfReproduceBehaviour { get; private set; }


        public ConditionalReproduceBehaviour(IReproduceConditon reproduceConditon, ISelfReproduceBehaviour reproduceBehaviour)
        {
            if (reproduceConditon == null) throw new ArgumentNullException(nameof(reproduceConditon));
            if (reproduceBehaviour == null) throw new ArgumentNullException(nameof(reproduceBehaviour));

            ReproduceConditon = reproduceConditon;
            SelfReproduceBehaviour = reproduceBehaviour;
        }


        public IReadOnlyCollection<WordObject> Reproduce(WordObject source)
        {
            return ReproduceConditon.IsMet()
                ? SelfReproduceBehaviour.Reproduce(source)
                : Enumerable.Empty<WordObject>().ToList();
        }
    }
}