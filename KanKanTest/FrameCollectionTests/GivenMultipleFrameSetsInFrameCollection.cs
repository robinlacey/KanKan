using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.KarassFrame;
using NUnit.Framework;

namespace KanKanTest.FrameCollectionTests
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
                Assert.True(kanKan.CurrentState.NextFrames.Contains(setOneFrame));
                Assert.True(kanKan.CurrentState.NextFrames.Contains(setTwoFrame));
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
                Assert.True(kanKan.CurrentState.NextFrames.Contains(setOneFrameOne));
                Assert.True(kanKan.CurrentState.NextFrames.Contains(setTwoFrameOne));

                Assert.False(kanKan.CurrentState.NextFrames.Contains(setOneFrameTwo));
                Assert.False(kanKan.CurrentState.NextFrames.Contains(setTwoFrameTwo));

                Assert.False(kanKan.CurrentState.NextFrames.Contains(setOneFrameTwo));
                Assert.False(kanKan.CurrentState.NextFrames.Contains(setTwoFrameTwo));
            }
        }
    }
}