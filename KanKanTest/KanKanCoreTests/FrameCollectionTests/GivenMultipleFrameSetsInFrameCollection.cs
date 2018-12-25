using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.FrameCollectionTests
{
    public class GivenMultipleFrameSetsInFrameCollection
    {
        private static KarassFactory KarassFactory => new KarassFactory();
             
        public class WhenThereIsOneFrame
        {
            private readonly MockFramesFactory _mockFramesFactory = new MockFramesFactory(new FrameFactoryDummy());

            [Test]
            public void ThenBothFramesAreInCurrentFrames()
            {
                FrameRequest setOneFrame = _mockFramesFactory.GetInvalidFrameRequest();
                FrameRequest setTwoFrame = _mockFramesFactory.GetInvalidFrameRequest();
                
                Karass karass = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            setOneFrame
                        },
                        new[]
                        {
                            setTwoFrame
                        }
                    });
                KanKan kanKan = new KanKan(karass,  new FrameFactory(new KarassDependencies()));
                Assert.True(kanKan.NextFrames.Contains(setOneFrame));
                Assert.True(kanKan.NextFrames.Contains(setTwoFrame));
            }
        }

        public class WhenThereAreMultipleFrames
        {
            private readonly MockFramesFactory _mockFramesFactory = new MockFramesFactory(new FrameFactoryDummy());

            [Test]
            public void ThenOnlyTheFirstFramesAreInCurrentFrames()
            {
                FrameRequest setOneFrameOne = _mockFramesFactory.GetInvalidFrameRequest();
                FrameRequest setOneFrameTwo = _mockFramesFactory.GetInvalidFrameRequest();
                FrameRequest setOneFrameThree = _mockFramesFactory.GetInvalidFrameRequest();


                FrameRequest setTwoFrameOne = _mockFramesFactory.GetInvalidFrameRequest();
                FrameRequest setTwoFrameTwo = _mockFramesFactory.GetInvalidFrameRequest();
                FrameRequest setTwoFrameThree = _mockFramesFactory.GetInvalidFrameRequest();
                Karass karass = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            setOneFrameOne,
                            setOneFrameTwo,
                            setOneFrameThree
                        },
                        new[]
                        {
                            setTwoFrameOne,
                            setTwoFrameTwo,
                            setTwoFrameThree
                        }
                    });
                KanKan kanKan = new KanKan(karass, new FrameFactoryDummy());
                Assert.True(kanKan.NextFrames.Contains(setOneFrameOne));
                Assert.True(kanKan.NextFrames.Contains(setTwoFrameOne));

                Assert.False(kanKan.NextFrames.Contains(setOneFrameTwo));
                Assert.False(kanKan.NextFrames.Contains(setTwoFrameTwo));

                Assert.False(kanKan.NextFrames.Contains(setOneFrameTwo));
                Assert.False(kanKan.NextFrames.Contains(setTwoFrameTwo));
            }
        }
    }
}