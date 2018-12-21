//using System;
//using System.Collections.Generic;
//using KanKanCore;
//using KanKanCore.Factories;
//using KanKanCore.Karass;
//using KanKanTest.Mocks.Dependencies;
//using KanKanTest.Mocks.UAction;
//using NUnit.Framework;
//
//namespace KanKanTest.SetupTeardownTests
//{
//    [TestFixture]
//    public class KanKanSetupAndTeardownTests
//    {
//        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());
//
//        public class GivenNoFrames
//        {
//
//            [Test]
//            public void SetupAndTearDownAreRun()
//            {
//                int setupCounter = 0;
//                int teardownCounter = 0;
//                Action setup = () => { setupCounter++; };
//                Action teardown = () => { teardownCounter++; };
//                Karass testKarass = KarassFactory.Get(CreateActionListWith(setup), CreateActionListWith(teardown),
//                    new List<FrameRequest[]>());
//
//                KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
//
//                actionRunner.MoveNext();
//                Assert.True(setupCounter == 1);
//                Assert.True(teardownCounter == 1);
//            }
//        }
//
//        public class GivenOneSetOfFrames
//        {
//            public class WithOneFrame
//            {
//                private bool _frameRun;
//
//                bool FrameSpy(string message)
//                {
//                    _frameRun = true;
//                    return true;
//                }
//
//                private List<FrameRequest[]> Frames => new List<FrameRequest[]>
//                {
//                    new FrameRequest[]
//                    {
//                        FrameSpy
//                    }
//                };
//
//                [Test]
//                public void SetupAndTeardownIsRun()
//                {
//                    int setupCounter = 0;
//                    int teardownCounter = 0;
//                    Action setup = () => { setupCounter++; };
//                    Action teardown = () => { teardownCounter++; };
//                    Karass testKarass = KarassFactory.Get(CreateActionListWith(setup), CreateActionListWith(teardown), Frames);
//
//                    KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
//
//                    actionRunner.MoveNext();
//
//                    Assert.True(setupCounter == 1);
//                    Assert.True(teardownCounter == 1);
//                    Assert.True(_frameRun);
//                }
//            }
//        }
//
//        public class GivenMultipleSetupAndTeardownArrays
//        {
//            public class WhenSetupIsGivenAnIndex
//            {
//                [Test]
//                public void ThenCorrectSetupActionsAreCalled_ExampleOne()
//                {
//                    bool karassOneSetupCalled = false;
//
//                    void KarassOneSetupSpy()
//                    {
//                        karassOneSetupCalled = true;
//                    }
//
//                    bool karassTwoSetupCalled = false;
//
//                    void KarassTwoSetupSpy()
//                    {
//                        karassTwoSetupCalled = true;
//                    }
//
//                    Karass karassOne = KarassFactory.Get(CreateActionListWith(KarassOneSetupSpy), new List<List<Action>>(),
//                        new List<FrameRequest[]>());
//                    Karass karassTwo = KarassFactory.Get(CreateActionListWith(KarassTwoSetupSpy), new List<List<Action>>(),
//                        new List<FrameRequest[]>());
//
//                    Karass combinedKarass = karassOne + karassTwo;
//
//                    combinedKarass.Setup(0);
//                    Assert.True(karassOneSetupCalled);
//                    Assert.False(karassTwoSetupCalled);
//                }
//
//                [Test]
//                public void ThenCorrectSetupActionsAreCalled_ExampleTwo()
//                {
//                    bool karassOneSetupCalled = false;
//
//                    void KarassOneSetupSpy()
//                    {
//                        karassOneSetupCalled = true;
//                    }
//
//                    bool karassTwoSetupCalled = false;
//
//                    void KarassTwoSetupSpy()
//                    {
//                        karassTwoSetupCalled = true;
//                    }
//
//                    Karass karassOne = KarassFactory.Get(CreateActionListWith(KarassOneSetupSpy), new List<List<Action>>(),
//                        new List<FrameRequest[]>());
//                    Karass karassTwo = KarassFactory.Get(CreateActionListWith(KarassTwoSetupSpy), new List<List<Action>>(),
//                        new List<FrameRequest[]>());
//
//                    Karass combinedKarass = karassOne + karassTwo;
//
//                    combinedKarass.Setup(1);
//                    Assert.False(karassOneSetupCalled);
//                    Assert.True(karassTwoSetupCalled);
//                }
//            }
//
//            public class WhenTeardownIsGivenAnIndex
//            {
//                [Test]
//                public void ThenCorrectTeardownActionsAreCalled_ExampleOne()
//                {
//                    bool karassOneTeardownCalled = false;
//
//                    void KarassOneTeardownSpy()
//                    {
//                        karassOneTeardownCalled = true;
//                    }
//
//                    bool karassTwoTeardownCalled = false;
//
//                    void KarassTwoTeardownSpy()
//                    {
//                        karassTwoTeardownCalled = true;
//                    }
//
//                    Karass karassOne = KarassFactory.Get(new List<List<Action>>(), CreateActionListWith(KarassOneTeardownSpy),
//                        new List<Func<string, bool>[]>());
//                    Karass karassTwo =KarassFactory.Get(new List<List<Action>>(), CreateActionListWith(KarassTwoTeardownSpy),
//                        new List<Func<string, bool>[]>());
//
//                    Karass combinedKarass = karassOne + karassTwo;
//
//                    combinedKarass.Teardown(0);
//                    Assert.True(karassOneTeardownCalled);
//                    Assert.False(karassTwoTeardownCalled);
//                }
//
//                [Test]
//                public void ThenCorrectTeardownActionsAreCalled_ExampleTwo()
//                {
//                    bool karassOneTeardownCalled = false;
//
//                    void KarassOneTeardownSpy()
//                    {
//                        karassOneTeardownCalled = true;
//                    }
//
//                    bool karassTwoTeardownCalled = false;
//
//                    void KarassTwoTeardownSpy()
//                    {
//                        karassTwoTeardownCalled = true;
//                    }
//
//                    Karass karassOne = KarassFactory.Get(new List<List<Action>>(), CreateActionListWith(KarassOneTeardownSpy),
//                        new List<Func<string, bool>[]>());
//                    Karass karassTwo = KarassFactory.Get(new List<List<Action>>(), CreateActionListWith(KarassTwoTeardownSpy),
//                        new List<Func<string, bool>[]>());
//
//                    Karass combinedKarass = karassOne + karassTwo;
//
//                    combinedKarass.Teardown(1);
//                    Assert.False(karassOneTeardownCalled);
//                    Assert.True(karassTwoTeardownCalled);
//                }
//            }
//        }
//
//        private static List<List<Action>> CreateActionListWith(Action a) =>
//            new List<List<Action>> {new List<Action> {a}};
//    }
//}