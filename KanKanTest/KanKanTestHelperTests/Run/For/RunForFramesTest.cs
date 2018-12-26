using System;
using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame.FrameStruct;
using KanKanTest.KanKanTestHelperTests.Mocks;
using KanKanTestHelper;
using KanKanTestHelper.Run;
using NUnit.Framework;

namespace KanKanTest.KanKanTestHelperTests.Run.For
{
    public class RunForFramesTest
    {
        private static FrameRequest[] CreateFrames(int frameCount)
        {
            FrameRequest[] frames = new FrameRequest[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = new FrameRequest(new FrameStructDummy());
            }

            return frames;
        }

        public class GivenFrameAsZero
        {
            public class WhenThereAreNoFramesKanKan
            {
                [Test]
                public void ThenReturnKanKanStateWithEmptyNextAndLastFrames()
                {
                    KarassFactory karassFactory = new KarassFactory();
                    Karass karass = karassFactory.Get(new List<Action>(), new List<Action>(),
                        new List<FrameRequest[]>());
                    KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                    TestHelper kanKanTestHelper = new TestHelper(new RunKanKan(kankan, new RunUntilDummy()), kankan, new FrameFactoryDummy());

                    IKanKanCurrentState currentState = kanKanTestHelper.Run.For(0);

                    Assert.True(currentState.NextFrames.Count == 0);
                    Assert.True(currentState.LastFrames.Count == 0);
                }
            }

            public class WhenThereAreMultipleFrames
            {
                public class InUncombinedKarass
                {
                    [TestCase(1)]
                    [TestCase(42)]
                    public void ThenReturnKanKanStateWithOneNextAndNoLastFrames(int frameCount)
                    {
                        Karass karass = new KarassFactory().Get(new List<Action>(), new List<Action>(),
                            new List<FrameRequest[]>()
                            {
                                CreateFrames(frameCount)
                            });

                        KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                        TestHelper kanKanTestHelper =
                            new TestHelper(new RunKanKan(kankan, new RunUntilDummy()), kankan, new FrameFactoryDummy());

                        IKanKanCurrentState currentState = kanKanTestHelper.Run.For(0);

                        Assert.True(currentState.NextFrames.Count == 1);
                        Assert.True(currentState.LastFrames.Count == 0);
                    }
                }

                public class InCombinedKarass
                {
                    [TestCase(2, 10)]
                    [TestCase(8, 20)]
                    public void ThenReturnKanKanStateWithOneNextAndNoLastFrames(int frameCount,
                        int numberOfCombinedKarass)
                    {
                        Karass karass = GetKarassWithFrames(frameCount);

                        for (int i = 0; i < numberOfCombinedKarass - 1; i++)
                        {
                            karass += GetKarassWithFrames(frameCount);
                        }

                        KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                        TestHelper kanKanTestHelper =
                            new TestHelper(new RunKanKan(kankan, new RunUntilDummy()), kankan, new FrameFactoryDummy());

                        IKanKanCurrentState currentState = kanKanTestHelper.Run.For(0);
                        Assert.True(currentState.NextFrames.Count == numberOfCombinedKarass);
                        Assert.True(currentState.LastFrames.Count == 0);
                    }

                    private static Karass GetKarassWithFrames(int frameCount)
                    {
                        return new KarassFactory().Get(new List<Action>(), new List<Action>(),
                            new List<FrameRequest[]>()
                            {
                                CreateFrames(frameCount)
                            });
                    }
                }
            }
        }

        public class GivenFrameWithinFrameCount
        {
            public class WhenThereAreNoFramesKanKan
            {
                [TestCase(5)]
                [TestCase(555)]
                public void ThenReturnKanKanStateWithEmptyNextAndLastFrames(int frame)
                {
                    KarassFactory karassFactory = new KarassFactory();
                    Karass karass = karassFactory.Get(new List<Action>(), new List<Action>(),
                        new List<FrameRequest[]>());
                    KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                    TestHelper kanKanTestHelper = new TestHelper(new RunKanKan(kankan, new RunUntilDummy()), kankan,new FrameFactoryDummy());

                    IKanKanCurrentState currentState = kanKanTestHelper.Run.For(frame);

                    Assert.True(currentState.NextFrames.Count == 0);
                    Assert.True(currentState.LastFrames.Count == 0);
                }
            }

            public class WhenThereAreMultipleFrames
            {
                public class InUncombinedKarass
                {
                    [TestCase(22)]
                    [TestCase(42)]
                    public void ThenReturnKanKanStateWithOneNextAndNoLastFrames(int frameCount)
                    {
                        Karass karass = new KarassFactory().Get(new List<Action>(), new List<Action>(),
                            new List<FrameRequest[]>()
                            {
                                CreateFrames(frameCount)
                            });

                        KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                        TestHelper kanKanTestHelper =
                            new TestHelper(new RunKanKan(kankan, new RunUntilDummy()), kankan,new FrameFactoryDummy());


                        Assert.True(kanKanTestHelper.Run.For(0).NextFrames.Count == 1);
                        Assert.True(kanKanTestHelper.Run.For(0).LastFrames.Count == 0);


                        Assert.True(kanKanTestHelper.Run.For(frameCount - 2).NextFrames.Count == 1);
                        Assert.True(kanKanTestHelper.Run.For(frameCount - 2).LastFrames.Count == 1);

                        Assert.True(kanKanTestHelper.Run.For(frameCount).NextFrames.Count == 0);
                        Assert.True(kanKanTestHelper.Run.For(frameCount).LastFrames.Count == 1);
                    }
                }

                public class InCombinedKarass
                {
                    [TestCase(1, 4, 15)]
                    [TestCase(2, 12, 34)]
                    public void ThenReturnKanKanStateWithOneNextAndNoLastFrames(int frame, int frameCount,
                        int numberOfCombinedKarass)
                    {
                        Karass karass = GetKarassWithFrames(frameCount);

                        for (int i = 0; i < numberOfCombinedKarass - 1; i++)
                        {
                            karass += GetKarassWithFrames(frameCount);
                        }

                        KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                        TestHelper kanKanTestHelper =
                            new TestHelper(new RunKanKan(kankan, new RunUntilDummy()), kankan,new FrameFactoryDummy());

                        Assert.True(kanKanTestHelper.Run.For(0).NextFrames.Count == numberOfCombinedKarass);
                        Assert.True(kanKanTestHelper.Run.For(0).LastFrames.Count == 0);


                        Assert.True(kanKanTestHelper.Run.For(frameCount - 2).NextFrames.Count ==
                                    numberOfCombinedKarass);
                        Assert.True(kanKanTestHelper.Run.For(frameCount - 2).LastFrames.Count ==
                                    numberOfCombinedKarass);

                        Assert.True(kanKanTestHelper.Run.For(frameCount).NextFrames.Count == 0);
                        Assert.True(kanKanTestHelper.Run.For(frameCount).LastFrames.Count ==
                                    numberOfCombinedKarass);
                    }

                    private static Karass GetKarassWithFrames(int frameCount)
                    {
                        return new KarassFactory().Get(new List<Action>(), new List<Action>(),
                            new List<FrameRequest[]>()
                            {
                                CreateFrames(frameCount)
                            });
                    }
                }
            }
        }
       
    }
}