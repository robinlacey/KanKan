using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.UAction;
using NUnit.Framework;

namespace KanKanTest.ProgressionTests
{
    public class GivenMultipleFrameSetsWhenNextFramesIsUpdated
    {
        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());

        public class WhenThereAreTwoEqualFrameSets
        {
            [Test]
            public void ThenNextFramesIsCorrectlyUpdated()
            {
                bool setOneFrameOneRun = false;

                bool SetOneFrameOneSpy(string s)
                {
                    setOneFrameOneRun = true;
                    return true;
                }

                bool setOneFrameTwoRun = false;

                bool SetOneFrameTwoSpy(string s)
                {
                    setOneFrameTwoRun = true;
                    return true;
                }

                bool setTwoFrameOneRun = false;

                bool SetTwoFrameOneSpy(string s)
                {
                    setTwoFrameOneRun = true;
                    return true;
                }

                bool setTwoFrameTwoRun = false;

                bool SetTwoFrameTwoSpy(string s)
                {
                    setTwoFrameTwoRun = true;
                    return true;
                }


                Karass karass = KarassFactory.Get(new List<List<Action>>(), new List<List<Action>>(),
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            SetOneFrameOneSpy,
                            SetOneFrameTwoSpy
                        },
                        new Func<string, bool>[]
                        {
                            SetTwoFrameOneSpy,
                            SetTwoFrameTwoSpy
                        }
                    });

                KanKan kankan = new KanKan(karass, new KarassMessageDummy());
                Assert.True(kankan.CurrentState.NextFrames.Count == 2);
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetOneFrameOneSpy));
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetTwoFrameOneSpy));

                kankan.MoveNext();
                Assert.True(kankan.CurrentState.NextFrames.Count == 2);
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetOneFrameTwoSpy));
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetTwoFrameTwoSpy));

                Assert.True(setOneFrameOneRun);
                Assert.False(setOneFrameTwoRun);
                Assert.True(setTwoFrameOneRun);
                Assert.False(setTwoFrameTwoRun);

                kankan.MoveNext();

                Assert.False(kankan.CurrentState.NextFrames.Any());

                Assert.True(setOneFrameOneRun);
                Assert.True(setOneFrameTwoRun);
                Assert.True(setTwoFrameOneRun);
                Assert.True(setTwoFrameTwoRun);
            }
        }

        public class WhenThereAreMultipleUnequalFrameSets
        {
            [Test]
            public void ThenNextFramesIsUpdatedCorrectly()
            {
                bool setOneFrameOneRun = false;

                bool SetOneFrameOneSpy(string s)
                {
                    setOneFrameOneRun = true;
                    return true;
                }

                bool setOneFrameTwoRun = false;

                bool SetOneFrameTwoSpy(string s)
                {
                    setOneFrameTwoRun = true;
                    return true;
                }

                bool setTwoFrameOneRun = false;

                bool SetTwoFrameOneSpy(string s)
                {
                    setTwoFrameOneRun = true;
                    return true;
                }

                bool setTwoFrameTwoRun = false;

                bool SetTwoFrameTwoSpy(string s)
                {
                    setTwoFrameTwoRun = true;
                    return true;
                }

                bool setTwoFrameThreeRun = false;

                bool SetTwoFrameThreeSpy(string s)
                {
                    setTwoFrameThreeRun = true;
                    return true;
                }


                bool setThreeFrameOneRun = false;

                bool SetThreeFrameOneSpy(string s)
                {
                    setThreeFrameOneRun = true;
                    return true;
                }

                bool setThreeFrameTwoRun = false;

                bool SetThreeFrameTwoSpy(string s)
                {
                    setThreeFrameTwoRun = true;
                    return true;
                }

                bool setThreeFrameThreeRun = false;

                bool SetThreeFrameThreeSpy(string s)
                {
                    setThreeFrameThreeRun = true;
                    return true;
                }

                bool setThreeFrameFourRun = false;

                bool SetThreeFrameFourSpy(string s)
                {
                    setThreeFrameFourRun = true;
                    return true;
                }


                Karass karass = KarassFactory.Get(new List<List<Action>>(), new List<List<Action>>(),
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            SetOneFrameOneSpy,
                            SetOneFrameTwoSpy
                        },
                        new Func<string, bool>[]
                        {
                            SetTwoFrameOneSpy,
                            SetTwoFrameTwoSpy,
                            SetTwoFrameThreeSpy
                        },
                        new Func<string, bool>[]
                        {
                            SetThreeFrameOneSpy,
                            SetThreeFrameTwoSpy,
                            SetThreeFrameThreeSpy,
                            SetThreeFrameFourSpy
                        }
                    });

                KanKan kankan = new KanKan(karass, new KarassMessageDummy());
                Assert.True(kankan.CurrentState.NextFrames.Count == 3);
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetOneFrameOneSpy));
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetTwoFrameOneSpy));
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetThreeFrameOneSpy));

                kankan.MoveNext();
                Assert.True(kankan.CurrentState.NextFrames.Count == 3);
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetOneFrameTwoSpy));
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetTwoFrameTwoSpy));
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetThreeFrameTwoSpy));

                Assert.True(setOneFrameOneRun);
                Assert.False(setOneFrameTwoRun);

                Assert.True(setTwoFrameOneRun);
                Assert.False(setTwoFrameTwoRun);
                Assert.False(setTwoFrameThreeRun);

                Assert.True(setThreeFrameOneRun);
                Assert.False(setThreeFrameTwoRun);
                Assert.False(setThreeFrameThreeRun);
                Assert.False(setThreeFrameFourRun);

                kankan.MoveNext();

                Assert.True(kankan.CurrentState.NextFrames.Count == 2);
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetTwoFrameThreeSpy));
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetThreeFrameThreeSpy));

                Assert.True(setOneFrameOneRun);
                Assert.True(setOneFrameTwoRun);

                Assert.True(setTwoFrameOneRun);
                Assert.True(setTwoFrameTwoRun);
                Assert.False(setTwoFrameThreeRun);

                Assert.True(setThreeFrameOneRun);
                Assert.True(setThreeFrameTwoRun);
                Assert.False(setThreeFrameThreeRun);
                Assert.False(setThreeFrameFourRun);


                kankan.MoveNext();

                Assert.True(kankan.CurrentState.NextFrames.Count == 1);
                Assert.True(kankan.CurrentState.NextFrames.Contains(SetThreeFrameFourSpy));

                Assert.True(setOneFrameOneRun);
                Assert.True(setOneFrameTwoRun);

                Assert.True(setTwoFrameOneRun);
                Assert.True(setTwoFrameTwoRun);
                Assert.True(setTwoFrameThreeRun);

                Assert.True(setThreeFrameOneRun);
                Assert.True(setThreeFrameTwoRun);
                Assert.True(setThreeFrameThreeRun);
                Assert.False(setThreeFrameFourRun);

                kankan.MoveNext();
                Assert.False(kankan.CurrentState.NextFrames.Any());

                Assert.True(setOneFrameOneRun);
                Assert.True(setOneFrameTwoRun);

                Assert.True(setTwoFrameOneRun);
                Assert.True(setTwoFrameTwoRun);
                Assert.True(setTwoFrameThreeRun);

                Assert.True(setThreeFrameOneRun);
                Assert.True(setThreeFrameTwoRun);
                Assert.True(setThreeFrameThreeRun);
                Assert.True(setThreeFrameFourRun);
            }
        }
    }
}