using System;
using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.KarassFrame;
using NUnit.Framework;

namespace KanKanTest.KarassFactoryTests
{
    public class KarassFactoryGivenAnArrayOfFrames
    {
        private static FrameRequest[] Frames = new FrameRequest[] { };

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

                DependenciesDummy dependencies = new DependenciesDummy();
                FrameFactoryDummy frameFactory = new FrameFactoryDummy();
                KarassFactory karassFactory = new KarassFactory();
                Karass karass = karassFactory.Get(SetupDummy, TearDownDummy, Frames);
                Assert.NotNull(karass);
                Assert.True(karass.SetupActions[0].Contains(SetupDummy));
                Assert.True(karass.TeardownActions[0].Contains(TearDownDummy));
                Assert.AreEqual(karass.FramesCollection[0], Frames);
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

                List<Action> setup = new List<Action>
                {
                    SetupDummyOne,
                    SetupDummyTwo
                };

                List<Action> teardown = new List<Action>
                {
                    TearDownDummyOne,
                    TearDownDummyTwo
                };

                DependenciesDummy dependencies = new DependenciesDummy();
                FrameFactoryDummy frameFactory = new FrameFactoryDummy();
                KarassFactory karassFactory = new KarassFactory();
                Karass karass = karassFactory.Get(setup, teardown, Frames);
                Assert.NotNull(karass);

                Assert.AreEqual(karass.SetupActions[0], setup);
                Assert.AreEqual(karass.TeardownActions[0], teardown);
                Assert.AreEqual(karass.FramesCollection[0], Frames);
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

                    List<Action> setupOne = new List<Action>
                    {
                        SetupOneDummyOne,
                        SetupOneDummyTwo
                    };
                    List<Action> setupTwo = new List<Action>
                    {
                        SetupTwoDummyOne,
                        SetupTwoDummyTwo
                    };

                    List<Action> teardownOne = new List<Action>
                    {
                        TearOneDownDummyOne,
                        TearOneDownDummyTwo
                    };
                    List<Action> teardownTwo = new List<Action>
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

                    DependenciesDummy dependencies = new DependenciesDummy();
                    FrameFactoryDummy frameFactory = new FrameFactoryDummy();
                    KarassFactory karassFactory = new KarassFactory();
                    Karass karass = karassFactory.Get(setup, teardown, Frames);
                    Assert.NotNull(karass);

                    Assert.AreEqual(karass.SetupActions[0], setupOne);
                    Assert.AreEqual(karass.SetupActions[1], setupTwo);

                    Assert.AreEqual(karass.SetupActions, setup);

                    Assert.AreEqual(karass.TeardownActions[0], teardownOne);
                    Assert.AreEqual(karass.TeardownActions[1], teardownTwo);

                    Assert.AreEqual(karass.TeardownActions, teardown);
                    Assert.AreEqual(karass.FramesCollection[0], Frames);

                }
            }
        }
    }
}