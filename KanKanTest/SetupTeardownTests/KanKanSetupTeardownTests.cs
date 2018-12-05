using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Karass;
using KanKanTest.Mocks.UAction;
using Xunit;
using Xunit.Abstractions;

namespace KanKanTest.SetupTeardownTests
{
    public class KanKanSetupAndTeardownTests
    {
        public class GivenNoFrames
        {
            private readonly ITestOutputHelper _outputHelper;

            public GivenNoFrames(ITestOutputHelper outputHelper)
            {
                _outputHelper = outputHelper;
            }

            [Fact]
            void SetupAndTearDownAreRun()
            {
                int setupCounter = 0;
                int teardownCounter = 0;
                Action setup = () => { setupCounter++; };
                Action teardown = () => { teardownCounter++; };
                Karass testKarass = new Karass(CreateActionListWith(setup), CreateActionListWith(teardown),
                    new List<Func<string, bool>[]>());

                KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());

                actionRunner.MoveNext();
                _outputHelper.WriteLine(teardownCounter.ToString());
                Assert.True(setupCounter == 1);
                Assert.True(teardownCounter == 1);
            }
        }

        public class GivenOneSetOfFrames
        {
            public class WithOneFrame
            {
                private bool _frameRun;

                bool FrameSpy(string message)
                {
                    _frameRun = true;
                    return true;
                }

                private List<Func<string, bool>[]> Frames => new List<Func<string, bool>[]>
                {
                    new Func<string, bool>[]
                    {
                        FrameSpy
                    }
                };

                [Fact]
                void SetupAndTeardownIsRun()
                {
                    int setupCounter = 0;
                    int teardownCounter = 0;
                    Action setup = () => { setupCounter++; };
                    Action teardown = () => { teardownCounter++; };
                    Karass testKarass = new Karass(CreateActionListWith(setup), CreateActionListWith(teardown), Frames);

                    KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());

                    actionRunner.MoveNext();

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
                [Fact]
                void ThenCorrectSetupActionsAreCalled_ExampleOne()
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

                    Karass karassOne = new Karass(CreateActionListWith(KarassOneSetupSpy), new List<List<Action>>(),
                        new List<Func<string, bool>[]>());
                    Karass karassTwo = new Karass(CreateActionListWith(KarassTwoSetupSpy), new List<List<Action>>(),
                        new List<Func<string, bool>[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    combinedKarass.Setup(0);
                    Assert.True(karassOneSetupCalled);
                    Assert.False(karassTwoSetupCalled);
                }

                [Fact]
                void ThenCorrectSetupActionsAreCalled_ExampleTwo()
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

                    Karass karassOne = new Karass(CreateActionListWith(KarassOneSetupSpy), new List<List<Action>>(),
                        new List<Func<string, bool>[]>());
                    Karass karassTwo = new Karass(CreateActionListWith(KarassTwoSetupSpy), new List<List<Action>>(),
                        new List<Func<string, bool>[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    combinedKarass.Setup(1);
                    Assert.False(karassOneSetupCalled);
                    Assert.True(karassTwoSetupCalled);
                }
            }

            public class WhenTeardownIsGivenAnIndex
            {
                [Fact]
                void ThenCorrectTeardownActionsAreCalled_ExampleOne()
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

                    Karass karassOne = new Karass(new List<List<Action>>(), CreateActionListWith(KarassOneTeardownSpy),
                        new List<Func<string, bool>[]>());
                    Karass karassTwo = new Karass(new List<List<Action>>(), CreateActionListWith(KarassTwoTeardownSpy),
                        new List<Func<string, bool>[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    combinedKarass.Teardown(0);
                    Assert.True(karassOneTeardownCalled);
                    Assert.False(karassTwoTeardownCalled);
                }

                [Fact]
                void ThenCorrectTeardownActionsAreCalled_ExampleTwo()
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

                    Karass karassOne = new Karass(new List<List<Action>>(), CreateActionListWith(KarassOneTeardownSpy),
                        new List<Func<string, bool>[]>());
                    Karass karassTwo = new Karass(new List<List<Action>>(), CreateActionListWith(KarassTwoTeardownSpy),
                        new List<Func<string, bool>[]>());

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