using System;
using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;
using Xunit;

namespace KanKanTest.KarassFactoryTests
{
    public class KarassFactoryTests
    {
        public class GivenAnArrayOfFrames
        {
            private static Func<string, bool>[] Frames = new Func<string, bool>[] { };

            public class WhenThereIsASingleSetupAndTeardown
            {
                [Fact]
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
                    Assert.Equal(karass.FramesCollection[0], Frames);
                    Assert.Equal(karass.Dependencies, dependencies);
                }
            }

            public class WhenThereAnArrayOfSetupAndTeardown
            {
                [Fact]
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

                    Assert.Equal(karass.SetupActions[0], setup);
                    Assert.Equal(karass.TeardownActions[0], teardown);

                    Assert.Equal(karass.FramesCollection[0], Frames);

                    Assert.Equal(karass.Dependencies, dependencies);
                }

                public class WhenThereAnNestedArrayOfSetupAndTeardown
                {
                    [Fact]
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

                        Assert.Equal(karass.SetupActions[0], setupOne);
                        Assert.Equal(karass.SetupActions[1], setupTwo);

                        Assert.Equal(karass.SetupActions, setup);

                        Assert.Equal(karass.TeardownActions[0], teardownOne);
                        Assert.Equal(karass.TeardownActions[1], teardownTwo);

                        Assert.Equal(karass.TeardownActions, teardown);
                        Assert.Equal(karass.FramesCollection[0], Frames);

                        Assert.Equal(karass.Dependencies, dependencies);
                    }
                }

                // When there is an array of Setups

                // When there is a nested array of setups
            }
        }

        public class GivenAnNestedArrayOfFrames
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
                [Fact]
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
                    Assert.Equal(karass.FramesCollection, Frames);

                    Assert.Equal(karass.Dependencies, dependencies);
                }
            }

            public class WhenThereAnArrayOfSetupAndTeardown
            {
                [Fact]
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

                    Assert.Equal(karass.SetupActions[0], setup);
                    Assert.Equal(karass.TeardownActions[0], teardown);

                    Assert.Equal(karass.FramesCollection, Frames);

                    Assert.Equal(karass.Dependencies, dependencies);
                }

                public class WhenThereAnNestedArrayOfSetupAndTeardown
                {
                    [Fact]
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

                        Assert.Equal(karass.SetupActions[0], setupOne);
                        Assert.Equal(karass.SetupActions[1], setupTwo);

                        Assert.Equal(karass.SetupActions, setup);

                        Assert.Equal(karass.TeardownActions[0], teardownOne);
                        Assert.Equal(karass.TeardownActions[1], teardownTwo);

                        Assert.Equal(karass.TeardownActions, teardown);
                        Assert.Equal(karass.FramesCollection, Frames);

                        Assert.Equal(karass.Dependencies, dependencies);
                    }
                }
            }
        }
    }
}