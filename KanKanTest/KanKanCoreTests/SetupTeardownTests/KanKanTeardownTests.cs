using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.KanKanCoreTests.Factories;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.SetupTeardownTests
{
    public class KanKanTeardownTests
    {
        [TestFixture]
        public class GivenOneFrameSet
        {
            public class WithOneFrame
            {
                private KarassFactory _karassFactory;
                private IDependencies _dependencies;
                private IFrameFactory _frameFactory;
                private MockFramesFactory _mockFramesFactory;

                [SetUp]
                public void Setup()
                {
                    _dependencies = new KarassDependencies();
                    _frameFactory = new FrameFactory(_dependencies);
                    _mockFramesFactory = new MockFramesFactory(_frameFactory);
                    _karassFactory = new KarassFactory();
                }

                [Test]
                public void TeardownActionsAreCalled()
                {
                    bool teardownCalled = false;
                    bool frameCalled = false;

                    bool FrameSpy(string message)
                    {
                        frameCalled = true;
                        return true;
                    }

                    void TeardownSpy()
                    {
                        teardownCalled = true;
                    }

                    FrameRequest frameRequest = _mockFramesFactory.GetValidFrameRequest(FrameSpy);
                    Karass karass = _karassFactory.Get(
                        new List<List<Action>>(),
                        CreateActionListWith(TeardownSpy),
                        new List<FrameRequest[]>
                        {
                            new[]
                            {
                                frameRequest
                            }
                        });
                    KanKan kankan = new KanKan(karass, _frameFactory);

                    kankan.MoveNext();
                    Assert.True(frameCalled);
                    kankan.MoveNext();
                    Assert.True(teardownCalled);
                }
            }

            public class WithMultipleFrames
            {
                private KarassFactory _karassFactory;
                private IDependencies _dependencies;
                private IFrameFactory _frameFactory;
                private MockFramesFactory _mockFramesFactory;

                [SetUp]
                public void Setup()
                {
                    _dependencies = new KarassDependencies();
                    _frameFactory = new FrameFactory(_dependencies);
                    _mockFramesFactory = new MockFramesFactory(_frameFactory);
                    _karassFactory = new KarassFactory();
                }

                [Test]
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

                    void TeardownSpy()
                    {
                        teardownCalled = true;
                    }

                    Karass karass = _karassFactory.Get(new List<List<Action>>(),
                        CreateActionListWith(TeardownSpy),
                        new List<FrameRequest[]>
                        {
                            new[]
                            {
                                _mockFramesFactory.GetValidFrameRequest(FrameOneSpy),
                                _mockFramesFactory.GetValidFrameRequest(FrameTwoSpy),
                                _mockFramesFactory.GetValidFrameRequest(FrameThreeSpy),
                            }
                        });
                    KanKan kankan = new KanKan(karass, _frameFactory);

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