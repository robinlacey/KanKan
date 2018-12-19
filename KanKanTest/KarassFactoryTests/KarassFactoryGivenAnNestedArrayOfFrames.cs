using System;
using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;
using NUnit.Framework;

namespace KanKanTest.KarassFactoryTests
{
    public class KarassFactoryGivenAnNestedArrayOfFrames
    {
        static bool FrameOne(string message) => true;
        static bool FrameTwo(string message) => true;

        private static List<Func<string, bool>[]> Frames = new List<Func<string, bool>[]>
        {
            new Func<string, bool>[]
            {
                FrameOne,
                FrameTwo
            }
        };

        public class WhenThereIsASingleSetupAndTeardown
        {
            [Test]
            public void ThenMethodsArePutIntoCorrectArrays()
            {
                void SetupDummy()
                {
                }

                void TearDownDummy()
                {
                }

                var dependencies = new DependenciesDummy();
                KarassFactory karassFactory = new KarassFactory(dependencies);
                Karass karass = karassFactory.Get(SetupDummy, TearDownDummy, Frames);
                Assert.NotNull(karass);
                Assert.True(karass.SetupActions[0].Contains(SetupDummy));
                Assert.True(karass.TeardownActions[0].Contains(TearDownDummy));
                Assert.AreEqual(karass.FramesCollection, Frames);

                Assert.AreEqual(karass.Dependencies, dependencies);
            }
        }

        public class WhenThereAnArrayOfSetupAndTeardown
        {
            [Test]
            public void ThenMethodsArePutIntoCorrectArrays()
            {
                void SetupDummyOne()
                {
                }

                void SetupDummyTwo()
                {
                }

                void TearDownDummyOne()
                {
                }

                void TearDownDummyTwo()
                {
                }

                var setup = new List<Action>
                {
                    SetupDummyOne,
                    SetupDummyTwo
                };

                var teardown = new List<Action>
                {
                    TearDownDummyOne,
                    TearDownDummyTwo
                };

                var dependencies = new DependenciesDummy();
                KarassFactory karassFactory = new KarassFactory(dependencies);
                Karass karass = karassFactory.Get(setup, teardown, Frames);
                Assert.NotNull(karass);

                Assert.AreEqual(karass.SetupActions[0], setup);
                Assert.AreEqual(karass.TeardownActions[0], teardown);

                Assert.AreEqual(karass.FramesCollection, Frames);

                Assert.AreEqual(karass.Dependencies, dependencies);
            }

            public class WhenThereAnNestedArrayOfSetupAndTeardown
            {
                [Test]
                public void ThenMethodsArePutIntoCorrectArrays()
                {
                    void SetupOneDummyOne()
                    {
                    }

                    void SetupOneDummyTwo()
                    {
                    }

                    void SetupTwoDummyOne()
                    {
                    }

                    void SetupTwoDummyTwo()
                    {
                    }

                    void TearOneDownDummyOne()
                    {
                    }

                    void TearOneDownDummyTwo()
                    {
                    }

                    void TearTwoDownDummyOne()
                    {
                    }

                    void TearTwoDownDummyTwo()
                    {
                    }

                    var setupOne = new List<Action>
                    {
                        SetupOneDummyOne,
                        SetupOneDummyTwo
                    };
                    var setupTwo = new List<Action>
                    {
                        SetupTwoDummyOne,
                        SetupTwoDummyTwo
                    };

                    var teardownOne = new List<Action>
                    {
                        TearOneDownDummyOne,
                        TearOneDownDummyTwo
                    };
                    var teardownTwo = new List<Action>
                    {
                        TearTwoDownDummyOne,
                        TearTwoDownDummyTwo
                    };
                    List<List<Action>> setup = new List<List<Action>>
                    {
                        setupOne,
                        setupTwo
                    };

                    List<List<Action>> teardown = new List<List<Action>>
                    {
                        teardownOne,
                        teardownTwo
                    };

                    var dependencies = new DependenciesDummy();
                    KarassFactory karassFactory = new KarassFactory(dependencies);
                    Karass karass = karassFactory.Get(setup, teardown, Frames);
                    Assert.NotNull(karass);

                    Assert.AreEqual(karass.SetupActions[0], setupOne);
                    Assert.AreEqual(karass.SetupActions[1], setupTwo);

                    Assert.AreEqual(karass.SetupActions, setup);

                    Assert.AreEqual(karass.TeardownActions[0], teardownOne);
                    Assert.AreEqual(karass.TeardownActions[1], teardownTwo);

                    Assert.AreEqual(karass.TeardownActions, teardown);
                    Assert.AreEqual(karass.FramesCollection, Frames);

                    Assert.AreEqual(karass.Dependencies, dependencies);
                }
            }
        }
    }
}