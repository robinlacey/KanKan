using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.Dependencies;
using NUnit.Framework;

namespace KanKanTest.FrameCollectionTests
{
    public class GivenOneFrameSetInFrameCollection
    {
        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());

        public class WhenThereIsOneFrame
        {
            [Test]
            public void ThenTheFrameIsInCurrentFrames()
            {
                bool Frame(string message) => true;
                Karass karass = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            Frame
                        }
                    });
                KanKan kanKan = new KanKan(karass, new KarassMessage());
                Assert.True(kanKan.CurrentState.NextFrames.Contains(Frame));
            }
        }

        public class WhenThereAreMultipleFrames
        {
            [Test]
            public void ThenOnlyContainsFirstFramew()
            {
                bool FrameOne(string message) => false;
                bool FrameTwo(string message) => false;
                bool FrameThree(string message) => false;
                bool FrameFour(string message) => false;
                Karass karass = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            FrameOne,
                            FrameTwo,
                            FrameThree,
                            FrameFour
                        }
                    });
                KanKan kanKan = new KanKan(karass, new KarassMessage());
                Assert.True(kanKan.CurrentState.NextFrames.Contains(FrameOne));
                Assert.False(kanKan.CurrentState.NextFrames.Contains(FrameTwo));
                Assert.False(kanKan.CurrentState.NextFrames.Contains(FrameThree));
                Assert.False(kanKan.CurrentState.NextFrames.Contains(FrameFour));
            }
        }
    }
}