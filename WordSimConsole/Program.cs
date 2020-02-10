using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Autofac;
using VirtualWord;
using VirtualWord.Behaviours.FightBehaviour;
using VirtualWord.Behaviours.MoveBehaviour;
using VirtualWord.Behaviours.ReproduceBehaviour;
using VirtualWord.Utils;
using VirtualWord.Utils.Factories;
using VirtualWord.WordObjects;
using VirtualWord.WordObjects.CollisionHandler;
using VirtualWord.WordObjects.CollisionHandler.Base;
using VirtualWord.WordObjectsUpdater;
using VirtualWord.World;
using VirtualWord.World.DataTypes;
using VirtualWord.World.Interfaces;

namespace WordSimConsole
{
    class Program
    {
        static void Main(string[] args)
        {                       
            var wordSize = new WordSize(Console.WindowWidth,Console.WindowHeight -1);
            IContainer container = BuildContainer(wordSize);
            
            var ticker = container.Resolve<ITicker>();
            var storer = container.Resolve<IWordObjectsProvider>();
            var putter = container.Resolve<IWordObjectsInserter>();

            var wordObjects1 = Enumerable.Range(0, 30).Select(x => container.Resolve<ConcreteAnimalSample>()).ToList();
            var wordObjects2 = Enumerable.Range(0, 5).Select(x => container.Resolve<ConcreteAnimalSample2>()).ToList();
            var wordObjects3 = Enumerable.Range(0, 2).Select(x => container.Resolve<SamplePlant>()).ToList();


            var wordObjects = new List<WordObject>(wordObjects1.Union<WordObject>(wordObjects2).Union(wordObjects3));                       
            wordObjects.ForEach(wo => putter.Insert(wo));

            MainLoop(storer, ticker);
        }

        private static void MainLoop(IWordObjectsProvider storer, ITicker ticker)
        {
            int requestedTicksCount = 1;
            while (requestedTicksCount!=0)
            {
                ResetConsole();
                Console.Write("How many iterations? ");
                
                requestedTicksCount = int.Parse(Console.ReadLine());
                Console.Clear();
                for (int i = 0; i < requestedTicksCount; i++)
                {
                    DrawWord(storer, true);
                    ticker.Tick();
                    DrawWord(storer, false);

                   // Thread.Sleep(50);
                }
                //DrawWord(storer, false);
            }
        }

        private static void ResetConsole()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
        }

        private static IContainer BuildContainer( WordSize wordSize)
        {
            ContainerBuilder cb = new ContainerBuilder();

            cb.RegisterGeneric(typeof(MovableCollisionBase<,>)).As(typeof(ICollisionHandler<,>));
            cb.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(ICollisionHandler<,>)))
                .AsClosedTypesOf(typeof(ICollisionHandler<,>)).AsImplementedInterfaces();

            
            cb.Register(context => new SimpleReproduceBehaviour(context.Resolve<IWordObjectsFactory>(),
                                                                MoveBehavioursFactory.GetAllMovesUpToDistance(1)
                                                                                     .PossibleInWord(wordSize)
                                                                                     .NonColliding(context.Resolve<IWorldDataProvider>()))).SingleInstance()                
              .Named<IReproduceBehaviour>("simpleReproduce");
           cb.RegisterDecorator<IReproduceBehaviour>((context, behaviour) => new ReproduceWithCooldown(behaviour),
                           "simpleReproduce").SingleInstance();
//            cb.RegisterType<DoNotReproduce>().AsImplementedInterfaces();

            cb.Register(context => new Word(wordSize,context.Resolve<IWordObjectsProvider>()))
                .AsImplementedInterfaces().SingleInstance();
            //cb.RegisterType<HandleForConcreteWordObjectsTypes>().AsImplementedInterfaces().SingleInstance();
            //cb.RegisterType<WordObjectsChangeChangesUpdater>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<WordObjectsContainer>().AsImplementedInterfaces().SingleInstance();

            cb.RegisterType<AutofacWordObjectsFactory>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<StrongerKillsWeaker>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<Random>().AsSelf().SingleInstance();
            cb.Register(ctx => new WorldDataStorer(wordSize)).AsImplementedInterfaces().SingleInstance();



            cb.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (WordObject))).AssignableTo<WordObject>().AsSelf();

            return cb.Build();
        }

        static void DrawWord(IWordObjectsProvider w,bool clear)
        {
            ConsoleColorForObjectType colorForObjectType = new ConsoleColorForObjectType();
            Console.CursorVisible = false;

            foreach (var wo in w.GetCurrentWordObjects())
            {
                Console.ForegroundColor = colorForObjectType.Get(wo);
                string output = clear ? " " : ConsoleWorldObjectRepresentation.Get(wo);
                Console.SetCursorPosition(wo.Position.X, wo.Position.Y + 1 );
                Console.Write(output);             
            }
        }
            
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                