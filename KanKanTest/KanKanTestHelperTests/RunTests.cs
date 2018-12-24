using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame.FrameStruct;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using KanKanTest.KanKanTestHelperTests.Mocks;
using KanKanTestHelper;
using KanKanTestHelper.Interface;
using KanKanTestHelper.Run;
using NUnit.Framework;
namespace KanKanTest.KanKanTestHelperTests
{
    public class FrameFactoryTests
    {
        [Test]
        public void KanKanTestHelperAssignedIFrameFactory()
        {
            IFrameFactory frameFactoryDummy = new FrameFactoryDummy();
            TestHelper kanKanTestHelper = new TestHelper(new RunKanKanDummy(), new KanKan(new KarassDummy(), frameFactoryDummy));
            Assert.AreSame(kanKanTestHelper.FrameFactory,frameFactoryDummy);
        }
    }
    public class RunTests
    {
        [Test]
        public void KanKanTestHelperHasAnIRunKanKan()
        {
            TestHelper kanKanTestHelper = new TestHelper(new RunKanKanDummy(), new KanKanDummy());
            Assert.True((IRunKanKan) kanKanTestHelper.Run !=null);
        }
        [Test]
        public void KanKanTestHelperAssignedIRunKanKan()
        {
            RunKanKanDummy runKanKanDummy = new RunKanKanDummy();
            TestHelper kanKanTestHelper = new TestHelper(runKanKanDummy,  new KanKanDummy());
            Assert.AreSame(kanKanTestHelper.Run,runKanKanDummy);
        }
        
        public class ForFramesTest
        {
            [Test]
            public void KanKanTestHelperForReturnsIKanKanCurrentState()
            {
                TestHelper kanKanTestHelper = new TestHelper(new RunKanKanDummy(), new KanKanDummy());
                IKanKanCurrentState currentState = kanKanTestHelper.Run.For(0);
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
                        TestHelper kanKanTestHelper = new TestHelper(new RunKanKan(kankan),kankan);
                        
                        IKanKanCurrentState currentState = kanKanTestHelper.Run.For(0);
                        
                        Assert.True(currentState.NextFrames.Count == 0);
                        Assert.True(currentState.LastFrames.Count == 0);
                    }
                }

                public class WhenThereAreMultipleFrames
                {
                    protected static FrameRequest[] CreateFrames(int frameCount)
                    {
                        FrameRequest[] frames = new FrameRequest[frameCount];
                        for (int i = 0; i < frameCount; i++)
                        {
                            frames[i] = new FrameRequest(new FrameStructDummy());
                        }

                        return frames;
                    }
                    
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
                            TestHelper kanKanTestHelper = new TestHelper(new RunKanKan(kankan), kankan);

                            IKanKanCurrentState currentState = kanKanTestHelper.Run.For(0);

                            Assert.True(currentState.NextFrames.Count == 1);
                            Assert.True(currentState.LastFrames.Count == 0);
                        }

                        
                    }
                    
                    public class InCombinedKarass
                    {
                        [TestCase(2,10)]
                        [TestCase(8,20)]
                        public void ThenReturnKanKanStateWithOneNextAndNoLastFrames(int frameCount, int numberOfCombinedKarass)
                        {

                            Karass karass = GetKarassWithFrames(frameCount);

                            for (int i = 0; i < numberOfCombinedKarass-1; i++)
                            {
                                
                                karass+= GetKarassWithFrames(frameCount);
                            }

                            KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                            TestHelper kanKanTestHelper = new TestHelper(new RunKanKan(kankan), kankan);

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
        }
    }
}