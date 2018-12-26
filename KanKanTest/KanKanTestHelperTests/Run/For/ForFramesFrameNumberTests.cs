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
    public class ForFramesFrameNumberTests
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
            Assert.True(currentState.Frame == 0);
        }
    
        public class InUncombinedKarass
        {
            private FrameRequest[] CreateFrames(int frameCount)
            {
                FrameRequest[] frames = new FrameRequest[frameCount];
                for (int i = 0; i < frameCount; i++)
                {
                    frames[i] = new FrameRequest(new FrameStructDummy());
                }
                return frames;
            }
            
            [TestCase(12)]
            [TestCase(43)]
            public void ThenReturnKanKanStateWithOneNextAndNoLastFrames(int frameCount)
            {
                Karass karass = new KarassFactory().Get(new List<Action>(), new List<Action>(),
                    new List<FrameRequest[]>
                    {
                        CreateFrames(frameCount)
                    });

                KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                TestHelper kanKanTestHelper = new TestHelper(new RunKanKan(kankan, new RunUntilDummy()), kankan,new FrameFactoryDummy());
                
                int returnFrameCount = kanKanTestHelper.Run.For(frameCount + 10).Frame;
                Assert.True(returnFrameCount == frameCount);
            }
        }

        public class InCombinedKarass
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
            
            [TestCase(1, 4, 15)]
            [TestCase(2, 12, 34)]
            public void ThenReturnKanKanStateWithOneNextAndNoLastFrames(
                int frame, 
                int frameCount,
                int numberOfCombinedKarass)
            {
                Karass karass = GetKarassWithFrames(frameCount);

                for (int i = 0; i < numberOfCombinedKarass - 1; i++)
                {
                    karass += GetKarassWithFrames(frameCount);
                }

                KanKan kankan = new KanKan(karass, new FrameFactoryDummy());
                TestHelper kanKanTestHelper = new TestHelper(new RunKanKan(kankan, new RunUntilDummy()), kankan, new FrameFactoryDummy());
                Assert.True( kanKanTestHelper.Run.For(frameCount + 10).Frame == frameCount);
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