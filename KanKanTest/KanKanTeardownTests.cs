using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Karass;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest
{
    public class KanKanTeardownTests
    {
        
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
                    Karass karass = new Karass(
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
                    Karass karass = new Karass(new List<List<Action>>(), 
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
        private static List<List<Action>> CreateActionListWith(Action a) => new List<List<Action>> { new List<Action> { a } };
//        public class GivenMultipleSetsWithOneFrame
//        {
//            // Bookmark
//            // This is where we have to make the jump to iterating over frame arrays 
//          
//            [Fact]
//            public void TeardownActionsAreCalled()
//            {
//                bool setOneTeardownCalled = false;
//                bool setOneFrameCalled = false;
//
//                bool SetOneFrameSpy(string message)
//                {
//                    setOneFrameCalled = true;
//                    return true;
//                }
//
//                Func<string, bool>[] setOneFrames = new Func<string, bool>[]
//                {
//                    SetOneFrameSpy
//                };
//
//
//                bool setTwoTeardownCalled = false;
//                bool setTwoFrameCalled = false;
//
//                bool SetTwoFrameSpy(string message)
//                {
//                    setTwoFrameCalled = true;
//                    return true;
//                }
//
//                Func<string, bool>[] setTwoFrames = {
//                    SetTwoFrameSpy
//                };
//
//
//                Karass karass = new Karass(new Action[0], new Action[]
//                {
//                    () => { setOneTeardownCalled = true; },
//                    () => { setTwoTeardownCalled = true; }
//                }, new List<Func<string, bool>[]> {setOneFrames, setTwoFrames});
//                KanKan kankan = new KanKan(karass, new KarassMessageDummy());
//
//                kankan.MoveNext();
//
//                Assert.True(setOneFrameCalled);
//                Assert.True(setTwoFrameCalled);
//                Assert.True(setOneTeardownCalled);
//                Assert.True(setTwoTeardownCalled);
//            }
//        }
    }
}