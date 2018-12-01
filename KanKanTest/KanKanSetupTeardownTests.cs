using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Karass;
using KanKanTest.Mocks.UAction;
using Xunit;
using Xunit.Abstractions;

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
                private readonly ITestOutputHelper _output;

                public WithOneFrame(ITestOutputHelper output)
                {
                    _output = output;
                }
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
                void SetupAndTeardownIsRun()
                {
                    int setupCounter = 0;
                    int teardownCounter = 0;
                    Action setup = () => { setupCounter++; };
                    Action teardown = () => { teardownCounter++; };
                    Karass testKarass = new Karass(new[] {setup}, new[] {teardown}, Frames);

                    KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
                    _output.WriteLine(actionRunner._frame.ToString() + 
                                      " " + (actionRunner._frame > actionRunner.Karass.FramesCollection[0].Length - 1));
                    actionRunner.MoveNext();
                    _output.WriteLine(actionRunner._frame.ToString()+ 
                                      " " + (actionRunner._frame > actionRunner.Karass.FramesCollection[0].Length - 1));
                    
                    Assert.True(setupCounter == 1);
                    Assert.True(teardownCounter == 1);
                    Assert.True(_frameRun);
                }
            }
        }
    }
}