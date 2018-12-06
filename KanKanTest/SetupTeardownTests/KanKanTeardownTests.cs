using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest.SetupTeardownTests
{
    public class KanKanTeardownTests
    {
        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());

        public class GivenOneFrameSet
        {
            public class WithOneFrame
            {
                [Fact]
                public void TeardownActionsAreCalled()
                {
                    bool teardownCalled = false;
                    bool frameCalled = false;

                    bool FrameSpy(string message)
                    {
                        frameCalled = true;
                        return true;
                    }

                    Func<string, bool>[] frames =
                    {
                        FrameSpy
                    };

                    void TeardownSpy()
                    {
                        teardownCalled = true;
                    }

                    Karass karass = KarassFactory.Get(
                        new List<List<Action>>(),
                        CreateActionListWith(TeardownSpy),
                        new List<Func<string, bool>[]> {frames});
                    KanKan kankan = new KanKan(karass, new KarassMessageDummy());

                    kankan.MoveNext();
                    Assert.True(frameCalled);
                    kankan.MoveNext();
                    Assert.True(teardownCalled);
                }
            }

            public class WithMultipleFrames
            {
                [Fact]
                public void TeardownActionsAreCalledOnLastFrame()
                {
                    bool teardownCalled = false;
                    bool frameOneCalled = false;

                    bool FrameOneSpy(string message)
                    {
                        frameOneCalled = true;
                        return true;
                    }

                    bool frameTwoCalled = false;

                    bool FrameTwoSpy(string message)
                    {
                        frameTwoCalled = true;
                        return true;
                    }

                    bool frameThreeCalled = false;

                    bool FrameThreeSpy(string message)
                    {
                        frameThreeCalled = true;
                        return true;
                    }


                    Func<string, bool>[] frames =
                    {
                        FrameOneSpy,
                        FrameTwoSpy,
                        FrameThreeSpy
                    };

                    void TeardownSpy()
                    {
                        teardownCalled = true;
                    }

                    Karass karass = KarassFactory.Get(new List<List<Action>>(),
                        CreateActionListWith(TeardownSpy),
                        new List<Func<string, bool>[]> {frames});
                    KanKan kankan = new KanKan(karass, new KarassMessageDummy());

                    kankan.MoveNext();
                    Assert.True(frameOneCalled);
                    Assert.False(teardownCalled);
                    kankan.MoveNext();
                    Assert.True(frameTwoCalled);
                    Assert.False(teardownCalled);
                    kankan.MoveNext();
                    Assert.True(frameThreeCalled);
                    Assert.True(teardownCalled);
                }
            }
        }

        private static List<List<Action>> CreateActionListWith(Action a) =>
            new List<List<Action>> {new List<Action> {a}};
    }
}