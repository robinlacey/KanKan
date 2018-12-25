using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.FrameCollectionTests
{
    public class GivenOneFrameSetInFrameCollection
    {
        private static KarassFactory KarassFactory => new KarassFactory();

        public class WhenThereIsOneFrame
        {
            private readonly MockFramesFactory _mockFramesFactory = new MockFramesFactory(new FrameFactoryDummy());

            [Test]
            public void ThenTheFrameIsInCurrentFrames()
            {
                FrameRequest frame = _mockFramesFactory.GetInvalidFrameRequest();

                Karass karass = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            frame
                        }
                    });
                KanKan kanKan = new KanKan(karass, new FrameFactoryDummy());
                Assert.True(kanKan.NextFrames.Contains(frame));
            }
        }

        public class WhenThereAreMultipleFrames
        {
            private readonly MockFramesFactory _mockFramesFactory = new MockFramesFactory(new FrameFactoryDummy());

            [Test]
            public void ThenOnlyContainsFirstFrame()
            {
                FrameRequest frameOne = _mockFramesFactory.GetInvalidFrameRequest();
                FrameRequest frameTwo = _mockFramesFactory.GetInvalidFrameRequest();
                FrameRequest frameThree = _mockFramesFactory.GetInvalidFrameRequest();
                FrameRequest frameFour = _mockFramesFactory.GetInvalidFrameRequest();
                Karass karass = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            frameOne,
                            frameTwo,
                            frameThree,
                            frameFour
                        }
                    });
                KanKan kanKan = new KanKan(karass, new FrameFactoryDummy());
                Assert.True(kanKan.NextFrames.Contains(frameOne));
                Assert.False(kanKan.NextFrames.Contains(frameTwo));
                Assert.False(kanKan.NextFrames.Contains(frameThree));
                Assert.False(kanKan.NextFrames.Contains(frameFour));
            }
        }
    }
}