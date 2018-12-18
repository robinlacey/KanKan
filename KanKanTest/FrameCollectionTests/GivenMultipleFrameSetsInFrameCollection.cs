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
    public class GivenMultipleFrameSetsInFrameCollection
    {
        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());

        public class WhenThereIsOneFrame
        {
            [Test]
            public void ThenBothFramesAreInCurrentFrames()
            {
                bool SetOneFrame(string message) => true;
                bool SetTwoFrame(string message) => true;
                Karass karass = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            SetOneFrame
                        },
                        new Func<string, bool>[]
                        {
                            SetTwoFrame
                        }
                    });
                KanKan kanKan = new KanKan(karass, new KarassMessage());
                Assert.True(kanKan.CurrentState.NextFrames.Contains(SetOneFrame));
                Assert.True(kanKan.CurrentState.NextFrames.Contains(SetTwoFrame));
            }
        }

        public class WhenThereAreMultipleFrames
        {
            [Test]
            public void ThenOnlyTheFirstFramesAreInCurrentFrames()
            {
                bool SetOneFrameOne(string message) => true;
                bool SetOneFrameTwo(string message) => true;
                bool SetOneFrameThree(string message) => true;


                bool SetTwoFrameOne(string message) => true;
                bool SetTwoFrameTwo(string message) => true;
                bool SetTwoFrameThree(string message) => true;
                Karass karass = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            SetOneFrameOne,
                            SetOneFrameTwo,
                            SetOneFrameThree
                        },
                        new Func<string, bool>[]
                        {
                            SetTwoFrameOne,
                            SetTwoFrameTwo,
                            SetTwoFrameThree
                        }
                    });
                KanKan kanKan = new KanKan(karass, new KarassMessage());
                Assert.True(kanKan.CurrentState.NextFrames.Contains(SetOneFrameOne));
                Assert.True(kanKan.CurrentState.NextFrames.Contains(SetTwoFrameOne));

                Assert.False(kanKan.CurrentState.NextFrames.Contains(SetOneFrameTwo));
                Assert.False(kanKan.CurrentState.NextFrames.Contains(SetTwoFrameTwo));

                Assert.False(kanKan.CurrentState.NextFrames.Contains(SetOneFrameTwo));
                Assert.False(kanKan.CurrentState.NextFrames.Contains(SetTwoFrameTwo));
            }
        }
    }
}