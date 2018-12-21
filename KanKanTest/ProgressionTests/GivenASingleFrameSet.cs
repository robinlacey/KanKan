//using System;
//using System.Collections.Generic;
//using System.Linq;
//using KanKanCore;
//using KanKanCore.Factories;
//using KanKanCore.Karass;
//using KanKanTest.Mocks.Dependencies;
//using KanKanTest.Mocks.UAction;
//using NUnit.Framework;
//
//namespace KanKanTest.ProgressionTests
//{
//    public class GivenASingleFrameSet : KanKanFrameProgressionTests
//    {
//        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());
//
//        public class WhenThereAreMultipleFrames
//        {
//            [Test]
//            public void ThenNextFramesCorrectlyProgressThroughFrames()
//            {
//                bool frameOneRun = false;
//
//                bool FrameOneSpy(string s)
//                {
//                    frameOneRun = true;
//                    return true;
//                }
//
//                bool frameTwoRun = false;
//
//                bool FrameTwoSpy(string s)
//                {
//                    frameTwoRun = true;
//                    return true;
//                }
//
//                bool frameThreeRun = false;
//
//                bool FrameThreeSpy(string s)
//                {
//                    frameThreeRun = true;
//                    return true;
//                }
//
//
//                bool setupRun = false;
//
//                void SetupSpy()
//                {
//                    setupRun = true;
//                }
//
//                bool tearDownRun = false;
//
//                void TeardownSpy()
//                {
//                    tearDownRun = true;
//                }
//
//                Karass karass = KarassFactory.Get(
//                    CreateActionListWith(SetupSpy),
//                    CreateActionListWith(TeardownSpy),
//                    new List<Func<string, bool>[]>
//                    {
//                        new Func<string, bool>[]
//                        {
//                            FrameOneSpy,
//                            FrameTwoSpy,
//                            FrameThreeSpy
//                        }
//                    });
//
//                KanKan kankan = new KanKan(karass, new KarassMessageDummy());
//
//                Assert.True(kankan.CurrentState.NextFrames.Contains(FrameOneSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameTwoSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameThreeSpy));
//
//                kankan.MoveNext();
//
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameOneSpy));
//                Assert.True(kankan.CurrentState.NextFrames.Contains(FrameTwoSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameThreeSpy));
//
//
//                CheckFirstFrame(setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);
//
//
//                kankan.MoveNext();
//
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameOneSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameTwoSpy));
//                Assert.True(kankan.CurrentState.NextFrames.Contains(FrameThreeSpy));
//
//
//                CheckSecondFrame(setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);
//
//                kankan.MoveNext();
//
//                Assert.False(kankan.CurrentState.NextFrames.Any());
//
//                CheckThirdFrame(setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);
//            }
//
//
//            private void CheckFirstFrame(bool setupRun, bool frameOneRun, bool tearDownRun, bool frameTwoRun,
//                bool frameThreeRun)
//            {
//                Assert.True(setupRun);
//                Assert.False(tearDownRun);
//                Assert.True(frameOneRun);
//                Assert.False(frameTwoRun);
//                Assert.False(frameThreeRun);
//            }
//
//            private void CheckSecondFrame(bool setupRun, bool frameOneRun, bool tearDownRun, bool frameTwoRun,
//                bool frameThreeRun)
//            {
//                Assert.True(setupRun);
//                Assert.False(tearDownRun);
//                Assert.True(frameOneRun);
//                Assert.True(frameTwoRun);
//                Assert.False(frameThreeRun);
//            }
//
//            private void CheckThirdFrame(bool setupRun, bool frameOneRun, bool tearDownRun, bool frameTwoRun,
//                bool frameThreeRun)
//            {
//                Assert.True(setupRun);
//                Assert.True(frameOneRun);
//                Assert.True(tearDownRun);
//                Assert.True(frameTwoRun);
//                Assert.True(frameThreeRun);
//            }
//        }
//
//
//        public class WhenThereIsACombinedKarass
//        {
//            [Test]
//            public void ThenFrameSetsAreRunIndependently()
//            {
//                bool setOneSetupRun = false;
//
//                void FrameSetOneSetupSpy()
//                {
//                    setOneSetupRun = true;
//                }
//
//                bool setOneTeardownRun = false;
//
//                void FrameSetOneTeardownSpy()
//                {
//                    setOneTeardownRun = true;
//                }
//
//                bool setOneFrameOneRun = false;
//
//                bool FrameSetOneFrameOneSpy(string message)
//                {
//                    setOneFrameOneRun = true;
//                    return true;
//                }
//
//                bool setOneFrameTwoRun = false;
//
//                bool FrameSetOneFrameTwoSpy(string message)
//                {
//                    setOneFrameTwoRun = true;
//                    return true;
//                }
//
//                bool setOneFrameThreeRun = false;
//
//                bool FrameSetOneFrameThreeSpy(string message)
//                {
//                    setOneFrameThreeRun = true;
//                    return true;
//                }
//
//
//                bool setTwoSetupRun = false;
//
//                void FrameSetTwoSetupSpy()
//                {
//                    setTwoSetupRun = true;
//                }
//
//                bool setTwoTeardownRun = false;
//
//                void FrameSetTwoTeardownSpy()
//                {
//                    setTwoTeardownRun = true;
//                }
//
//                bool setTwoFrameOneRun = false;
//
//                bool FrameSetTwoFrameOneSpy(string message)
//                {
//                    setTwoFrameOneRun = true;
//                    return true;
//                }
//
//                bool setTwoFrameTwoRun = false;
//
//                bool FrameSetTwoFrameTwoSpy(string message)
//                {
//                    setTwoFrameTwoRun = true;
//                    return true;
//                }
//
//
//                Karass karassOne = KarassFactory.Get(CreateActionListWith(FrameSetOneSetupSpy),
//                    CreateActionListWith(FrameSetOneTeardownSpy),
//                    new List<Func<string, bool>[]>
//                    {
//                        new Func<string, bool>[]
//                        {
//                            FrameSetOneFrameOneSpy,
//                            FrameSetOneFrameTwoSpy,
//                            FrameSetOneFrameThreeSpy
//                        }
//                    });
//
//                Karass karassTwo = KarassFactory.Get(CreateActionListWith(FrameSetTwoSetupSpy),
//                    CreateActionListWith(FrameSetTwoTeardownSpy),
//                    new List<Func<string, bool>[]>
//                    {
//                        new Func<string, bool>[]
//                        {
//                            FrameSetTwoFrameOneSpy,
//                            FrameSetTwoFrameTwoSpy
//                        }
//                    });
//
//                KanKan kankan = new KanKan(karassOne + karassTwo, new KarassMessageDummy());
//
//
//                Assert.True(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameOneSpy));
//                Assert.True(kankan.CurrentState.NextFrames.Contains(FrameSetTwoFrameOneSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameTwoSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetTwoFrameTwoSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameThreeSpy));
//
//
//                kankan.MoveNext();
//
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameOneSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetTwoFrameOneSpy));
//                Assert.True(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameTwoSpy));
//                Assert.True(kankan.CurrentState.NextFrames.Contains(FrameSetTwoFrameTwoSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameThreeSpy));
//
//                CheckFirstFrame(setOneSetupRun, setTwoSetupRun, setOneTeardownRun, setTwoTeardownRun, setOneFrameOneRun,
//                    setTwoFrameOneRun, setOneFrameTwoRun, setOneFrameThreeRun, setTwoFrameTwoRun);
//
//
//                kankan.MoveNext();
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameOneSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetTwoFrameOneSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameTwoSpy));
//                Assert.False(kankan.CurrentState.NextFrames.Contains(FrameSetTwoFrameTwoSpy));
//                Assert.True(kankan.CurrentState.NextFrames.Contains(FrameSetOneFrameThreeSpy));
//
//                CheckSecondFrame(setOneSetupRun, setTwoSetupRun, setOneTeardownRun, setTwoTeardownRun,
//                    setOneFrameOneRun, setTwoFrameOneRun, setOneFrameTwoRun, setTwoFrameTwoRun, setOneFrameThreeRun);
//
//
//                kankan.MoveNext();
//                Assert.False(kankan.CurrentState.NextFrames.Any());
//                CheckThirdFrame(setOneSetupRun, setTwoSetupRun, setOneTeardownRun, setTwoTeardownRun, setOneFrameOneRun,
//                    setTwoFrameOneRun, setOneFrameTwoRun, setTwoFrameTwoRun, setOneFrameThreeRun);
//            }
//
//            private static void CheckThirdFrame(bool setOneSetupRun, bool setTwoSetupRun, bool setOneTeardownRun,
//                bool setTwoTeardownRun, bool setOneFrameOneRun, bool setTwoFrameOneRun, bool setOneFrameTwoRun,
//                bool setTwoFrameTwoRun, bool setOneFrameThreeRun)
//            {
//                Assert.True(setOneSetupRun);
//                Assert.True(setTwoSetupRun);
//                Assert.True(setOneTeardownRun);
//                Assert.True(setTwoTeardownRun);
//
//
//                Assert.True(setOneFrameOneRun);
//                Assert.True(setTwoFrameOneRun);
//                Assert.True(setOneFrameTwoRun);
//                Assert.True(setTwoFrameTwoRun);
//                Assert.True(setOneFrameThreeRun);
//            }
//
//            private static void CheckSecondFrame(bool setOneSetupRun, bool setTwoSetupRun, bool setOneTeardownRun,
//                bool setTwoTeardownRun, bool setOneFrameOneRun, bool setTwoFrameOneRun, bool setOneFrameTwoRun,
//                bool setTwoFrameTwoRun, bool setOneFrameThreeRun)
//            {
//                Assert.True(setOneSetupRun);
//                Assert.True(setTwoSetupRun);
//                Assert.False(setOneTeardownRun);
//                Assert.True(setTwoTeardownRun);
//
//                Assert.True(setOneFrameOneRun);
//                Assert.True(setTwoFrameOneRun);
//                Assert.True(setOneFrameTwoRun);
//                Assert.True(setTwoFrameTwoRun);
//
//                Assert.False(setOneFrameThreeRun);
//            }
//
//            private static void CheckFirstFrame(bool setOneSetupRun, bool setTwoSetupRun, bool setOneTeardownRun,
//                bool setTwoTeardownRun, bool setOneFrameOneRun, bool setTwoFrameOneRun, bool setOneFrameTwoRun,
//                bool setOneFrameThreeRun, bool setTwoFrameTwoRun)
//            {
//                Assert.True(setOneSetupRun);
//                Assert.True(setTwoSetupRun);
//                Assert.False(setOneTeardownRun);
//                Assert.False(setTwoTeardownRun);
//
//
//                Assert.True(setOneFrameOneRun);
//                Assert.True(setTwoFrameOneRun);
//
//                Assert.False(setOneFrameTwoRun);
//                Assert.False(setOneFrameThreeRun);
//                Assert.False(setTwoFrameTwoRun);
//            }
//        }
//    }
//}