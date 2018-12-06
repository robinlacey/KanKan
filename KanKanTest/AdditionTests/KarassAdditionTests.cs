using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;
using Xunit;

namespace KanKanTest.AdditionTests
{
    public class KarassAdditionTests
    {
        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());
        
        private static List<List<Action>> CreateActionListWith(Action a) =>
            new List<List<Action>> {new List<Action> {a}};

        public class GivenMultipleSetupMethods
        {
            [Fact]
            public void WhenAddedThenMethodsAreInInList_ExampleOne()
            {
                Action setupOne = () => { };
                Karass karassOne = KarassFactory.Get(
                    CreateActionListWith(setupOne),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>());
                Action setupTwo = () => { };
                Karass karassTwo = KarassFactory.Get(
                    CreateActionListWith(setupTwo),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>());

                Karass combinedKarass = karassOne + karassTwo;

                Assert.True(combinedKarass.SetupActions.Count == 2);
                Assert.True(combinedKarass.SetupActions[0].Contains(setupOne));
                Assert.True(combinedKarass.SetupActions[1].Contains(setupTwo));
            }

            [Fact]
            public void WhenAddedThenMethodsAreInInList_ExampleTwo()
            {
                Action setupOne = () => { };
                Karass karassOne = KarassFactory.Get(
                    CreateActionListWith(setupOne),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>());
                Action setupTwo = () => { };
                Karass karassTwo = KarassFactory.Get(
                    CreateActionListWith(setupTwo),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>());

                Action setupThree = () => { };
                Karass karassThree =KarassFactory.Get(
                    CreateActionListWith(setupThree),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>());

                Action setupFour = () => { };
                Karass karassFour = KarassFactory.Get(
                    CreateActionListWith(setupFour),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>());


                Karass combinedKarass = karassOne + karassTwo + karassThree + karassFour;

                Assert.True(combinedKarass.SetupActions.Count == 4);
                Assert.True(combinedKarass.SetupActions[0].Contains(setupOne));
                Assert.True(combinedKarass.SetupActions[1].Contains(setupTwo));
                Assert.True(combinedKarass.SetupActions[2].Contains(setupThree));
                Assert.True(combinedKarass.SetupActions[3].Contains(setupFour));
            }
        }

        public class GivenMultipleTeardownMethods
        {
            [Fact]
            public void WhenAddedThenMethodsAreInInList_ExampleOne()
            {
                Action teardownOne = () => { };
                Karass karassOne = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownOne),
                    new List<Func<string, bool>[]>());
                Action teardownTwo = () => { };
                Karass karassTwo = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownTwo),
                    new List<Func<string, bool>[]>());

                Karass combinedKarass = karassOne + karassTwo;

                Assert.True(combinedKarass.TeardownActions.Count == 2);
                Assert.True(combinedKarass.TeardownActions[0].Contains(teardownOne));
                Assert.True(combinedKarass.TeardownActions[1].Contains(teardownTwo));
            }

            [Fact]
            public void WhenAddedThenMethodsAreInInList_ExampleTwo()
            {
                Action teardownOne = () => { };
                Karass karassOne = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownOne),
                    new List<Func<string, bool>[]>());
                Action teardownTwo = () => { };
                Karass karassTwo = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownTwo),
                    new List<Func<string, bool>[]>());

                Action teardownThree = () => { };
                Karass karassThree = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownThree),
                    new List<Func<string, bool>[]>());

                Action teardownFour = () => { };
                Karass karassFour = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownFour),
                    new List<Func<string, bool>[]>());


                Karass combinedKarass = karassOne + karassTwo + karassThree + karassFour;


                Assert.True(combinedKarass.TeardownActions.Count == 4);
                Assert.True(combinedKarass.TeardownActions[0].Contains(teardownOne));
                Assert.True(combinedKarass.TeardownActions[1].Contains(teardownTwo));
                Assert.True(combinedKarass.TeardownActions[2].Contains(teardownThree));
                Assert.True(combinedKarass.TeardownActions[3].Contains(teardownFour));
            }
        }

        public class GivenMultipleFrameSets
        {
            [Fact]
            public void WhenAddedThenFrameSetsContainArrays()
            {
                Func<string, bool>[] frameSetArrayOne = { };

                List<Func<string, bool>[]> frameSetOne = new List<Func<string, bool>[]>
                {
                    frameSetArrayOne
                };


                Func<string, bool>[] frameSetArrayTwo = { };

                List<Func<string, bool>[]> frameSetTwo = new List<Func<string, bool>[]>
                {
                    frameSetArrayTwo
                };

                Karass karassOne = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    frameSetOne);

                Karass karassTwo = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    frameSetTwo);

                Karass combinedKarass = karassOne + karassTwo;

                Assert.True(combinedKarass.FramesCollection.Count == 2);
                Assert.True(combinedKarass.FramesCollection.Contains(frameSetArrayOne));
                Assert.True(combinedKarass.FramesCollection.Contains(frameSetArrayTwo));
            }

            [Fact]
            public void WhenAddedThenFrameSetsContainFrames()
            {
                bool FrameSetOneFrameOne(string message)
                {
                    return true;
                }

                bool FrameSetOneFrameTwo(string message)
                {
                    return true;
                }

                Func<string, bool>[] frameSetArrayOne =
                {
                    FrameSetOneFrameOne,
                    FrameSetOneFrameTwo
                };

                List<Func<string, bool>[]> frameSetOne = new List<Func<string, bool>[]>
                {
                    frameSetArrayOne
                };


                bool FrameSetTwoFrameOne(string message)
                {
                    return true;
                }

                bool FrameSetTwoFrameTwo(string message)
                {
                    return true;
                }


                Func<string, bool>[] frameSetArrayTwo =
                {
                    FrameSetTwoFrameOne,
                    FrameSetTwoFrameTwo
                };

                List<Func<string, bool>[]> frameSetTwo = new List<Func<string, bool>[]>
                {
                    frameSetArrayTwo
                };

                Karass karassOne = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    frameSetOne);

                Karass karassTwo = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    frameSetTwo);

                Karass combinedKarass = karassOne + karassTwo;

                Assert.True(combinedKarass.FramesCollection.Count == 2);
                Assert.True(combinedKarass.FramesCollection.Contains(frameSetArrayOne));
                Assert.True(combinedKarass.FramesCollection.Contains(frameSetArrayTwo));

                Assert.True(combinedKarass.FramesCollection.Any(_ => _.Contains(FrameSetOneFrameOne)));
                Assert.True(combinedKarass.FramesCollection.Any(_ => _.Contains(FrameSetOneFrameTwo)));
                Assert.True(combinedKarass.FramesCollection.Any(_ => _.Contains(FrameSetTwoFrameOne)));
                Assert.True(combinedKarass.FramesCollection.Any(_ => _.Contains(FrameSetTwoFrameTwo)));
            }
        }
    }
}