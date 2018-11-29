using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Karass;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest
{
    public class KanKanSetupAndTeardownTests
    {
       
        public class GivenNoFrames
        {
            [Fact]
            void SetupAndTearDownAreRun()
            {
                int setupCounter = 0;
                int teardownCounter = 0;
                Action setup = () => { setupCounter++; };
                Action teardown = () => { teardownCounter++; };
                Karass testKarass = new Karass(new[] {setup}, new[] {teardown},
                    new List<Func<string, bool>[]>());

                KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());

                actionRunner.MoveNext();
                
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

                private List<Func<string, bool>[]> Frames => new List<Func<string, bool>[]>()
                {
                    new Func<string, bool>[]
                    {
                        FrameSpy
                    }
                };

                [Fact]
                void SetupIsRunAndTearDownIsNot()
                {
                    int setupCounter = 0;
                    int teardownCounter = 0;
                    Action setup = () => { setupCounter++; };
                    Action teardown = () => { teardownCounter++; };
                    Karass testKarass = new Karass(new[] {setup}, new[] {teardown}, Frames);

                    KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());

                    actionRunner.MoveNext();
                    Assert.True(setupCounter == 1);
                    Assert.True(teardownCounter == 0);

                    Assert.True(_frameRun);
                }
            }
        }
    }
}