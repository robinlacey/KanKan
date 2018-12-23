using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using NUnit.Framework;

namespace KanKanTest.SetupTeardownTests
{
    [TestFixture]
    public class KanKanSetupAndTeardownTests
    {
        public class GivenNoFrames
        {
            private KarassFactory KarassFactory => new KarassFactory();


            [Test]
            public void SetupAndTearDownAreRun()
            {
                int setupCounter = 0;
                int teardownCounter = 0;
                Action setup = () => { setupCounter++; };
                Action teardown = () => { teardownCounter++; };
                Karass testKarass = KarassFactory.Get(CreateActionListWith(setup), CreateActionListWith(teardown),
                    new List<FrameRequest[]>());

                KanKan kankan = new KanKan(testKarass, new FrameFactory(new KarassDependencies()));

                kankan.MoveNext();
                Assert.True(setupCounter == 1);
                Assert.True(teardownCounter == 1);
            }
        }

        public class GivenOneSetOfFrames
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


                private bool _frameRun;

                bool FrameSpy(string message)
                {
                    _frameRun = true;
                    return true;
                }

                private List<FrameRequest[]> Frames => new List<FrameRequest[]>
                {
                    new[]
                    {
                        _mockFramesFactory.GetValidFrameRequest(FrameSpy)
                    }
                };

                [Test]
                public void SetupAndTeardownIsRun()
                {
                    int setupCounter = 0;
                    int teardownCounter = 0;
                    Action setup = () => { setupCounter++; };
                    Action teardown = () => { teardownCounter++; };
                    Karass testKarass = _karassFactory.Get(CreateActionListWith(setup), CreateActionListWith(teardown),
                        Frames);

                    KanKan kanKan = new KanKan(testKarass,_frameFactory);

                    kanKan.MoveNext();

                    Assert.True(setupCounter == 1);
                    Assert.True(teardownCounter == 1);
                    Assert.True(_frameRun);
                }
            }
        }

        public class GivenMultipleSetupAndTeardownArrays
        {
            public class WhenSetupIsGivenAnIndex
            {
                private KarassFactory KarassFactory => new KarassFactory();


                [Test]
                public void ThenCorrectSetupActionsAreCalled_ExampleOne()
                {
                    bool karassOneSetupCalled = false;

                    void KarassOneSetupSpy()
                    {
                        karassOneSetupCalled = true;
                    }

                    bool karassTwoSetupCalled = false;

                    void KarassTwoSetupSpy()
                    {
                        karassTwoSetupCalled = true;
                    }

                    Karass karassOne = KarassFactory.Get(CreateActionListWith(KarassOneSetupSpy),
                        new List<List<Action>>(),
                        new List<FrameRequest[]>());
                    Karass karassTwo = KarassFactory.Get(CreateActionListWith(KarassTwoSetupSpy),
                        new List<List<Action>>(),
                        new List<FrameRequest[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    combinedKarass.Setup(0);
                    Assert.True(karassOneSetupCalled);
                    Assert.False(karassTwoSetupCalled);
                }

                [Test]
                public void ThenCorrectSetupActionsAreCalled_ExampleTwo()
                {
                    bool karassOneSetupCalled = false;

                    void KarassOneSetupSpy()
                    {
                        karassOneSetupCalled = true;
                    }

                    bool karassTwoSetupCalled = false;

                    void KarassTwoSetupSpy()
                    {
                        karassTwoSetupCalled = true;
                    }

                    Karass karassOne = KarassFactory.Get(CreateActionListWith(KarassOneSetupSpy),
                        new List<List<Action>>(),
                        new List<FrameRequest[]>());
                    Karass karassTwo = KarassFactory.Get(CreateActionListWith(KarassTwoSetupSpy),
                        new List<List<Action>>(),
                        new List<FrameRequest[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    combinedKarass.Setup(1);
                    Assert.False(karassOneSetupCalled);
                    Assert.True(karassTwoSetupCalled);
                }
            }

            public class WhenTeardownIsGivenAnIndex
            {
                private KarassFactory KarassFactory =>
                    new KarassFactory();

                [Test]
                public void ThenCorrectTeardownActionsAreCalled_ExampleOne()
                {
                    bool karassOneTeardownCalled = false;

                    void KarassOneTeardownSpy()
                    {
                        karassOneTeardownCalled = true;
                    }

                    bool karassTwoTeardownCalled = false;

                    void KarassTwoTeardownSpy()
                    {
                        karassTwoTeardownCalled = true;
                    }

                    Karass karassOne = KarassFactory.Get(new List<List<Action>>(),
                        CreateActionListWith(KarassOneTeardownSpy),
                        new List<FrameRequest[]>());
                    Karass karassTwo = KarassFactory.Get(new List<List<Action>>(),
                        CreateActionListWith(KarassTwoTeardownSpy),
                        new List<FrameRequest[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    combinedKarass.Teardown(0);
                    Assert.True(karassOneTeardownCalled);
                    Assert.False(karassTwoTeardownCalled);
                }

                [Test]
                public void ThenCorrectTeardownActionsAreCalled_ExampleTwo()
                {
                    bool karassOneTeardownCalled = false;

                    void KarassOneTeardownSpy()
                    {
                        karassOneTeardownCalled = true;
                    }

                    bool karassTwoTeardownCalled = false;

                    void KarassTwoTeardownSpy()
                    {
                        karassTwoTeardownCalled = true;
                    }

                    Karass karassOne = KarassFactory.Get(new List<List<Action>>(),
                        CreateActionListWith(KarassOneTeardownSpy),
                        new List<FrameRequest[]>());
                    Karass karassTwo = KarassFactory.Get(new List<List<Action>>(),
                        CreateActionListWith(KarassTwoTeardownSpy),
                        new List<FrameRequest[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    combinedKarass.Teardown(1);
                    Assert.False(karassOneTeardownCalled);
                    Assert.True(karassTwoTeardownCalled);
                }
            }
        }

        private static List<List<Action>> CreateActionListWith(Action a) =>
            new List<List<Action>> {new List<Action> {a}};
    }
}