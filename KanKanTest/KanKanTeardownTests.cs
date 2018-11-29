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
        public class GivenOneFrameSetAndOneFrame
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

                Func<string, bool>[] frames = new Func<string, bool>[]
                {
                    FrameSpy
                };

                Karass karass = new Karass(new Action[0], new Action[] {() => { teardownCalled = true; }},
                    new List<Func<string, bool>[]> {frames});
                KanKan kankan = new KanKan(karass, new KarassMessageDummy());

                kankan.MoveNext();
                Assert.True(frameCalled);
                kankan.MoveNext();
                Assert.True(teardownCalled);
            }
        }

        public class GivenMultipleSetsWithOneFrame
        {
            // Bookmark
            // This is where we have to make the jump to iterating over frame arrays 
          
            [Fact]
            public void TeadownActionsAreCalled()
            {
                bool setOneTeardownCalled = false;
                bool setOneFrameCalled = false;

                bool SetOneFrameSpy(string message)
                {
                    setOneFrameCalled = true;
                    return true;
                }

                Func<string, bool>[] setOneFrames = new Func<string, bool>[]
                {
                    SetOneFrameSpy
                };


                bool setTwoTeardownCalled = false;
                bool setTwoFrameCalled = false;

                bool SetTwoFrameSpy(string message)
                {
                    setTwoFrameCalled = true;
                    return true;
                }

                Func<string, bool>[] setTwoFrames = new Func<string, bool>[]
                {
                    SetTwoFrameSpy
                };


                Karass karass = new Karass(new Action[0], new Action[]
                {
                    () => { setOneTeardownCalled = true; },
                    () => { setTwoTeardownCalled = true; }
                }, new List<Func<string, bool>[]> {setOneFrames, setTwoFrames});
                KanKan kankan = new KanKan(karass, new KarassMessageDummy());

                kankan.MoveNext();

                Assert.True(setOneFrameCalled);
                Assert.True(setTwoFrameCalled);
                kankan.MoveNext();
                Assert.True(setOneTeardownCalled);
                Assert.True(setTwoTeardownCalled);
            }
        }
    }
}