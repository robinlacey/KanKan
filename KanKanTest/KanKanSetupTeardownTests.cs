using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Karass;
using KanKanTest.Mocks.UAction;
using Xunit;
using Xunit.Abstractions;

namespace KanKanTest
{
    public class KanKanSetupTeardownTests
    {
        [Fact]
        void SetupIsRunOnMoveNext()
        {
            int setupCounter = 0;
            Action setup = () => { setupCounter++; };
            Karass testKarass = new Karass(new[] {setup}, new Action[0],
                new List<Func<string, bool>[]>());

            KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
            actionRunner.MoveNext();
            Assert.True(setupCounter > 0);
        }


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

            public class WithMultipleFrames
            {
                private readonly ITestOutputHelper _output;

                public WithMultipleFrames(ITestOutputHelper output)
                {
                    _output = output;
                }
                bool FrameOne(string message) => true;
                bool FrameTwo(string message) => true;

                private List<Func<string, bool>[]> Frames => new List<Func<string, bool>[]>()
                {
                    new Func<string, bool>[]
                    {
                        FrameOne,
                        FrameTwo
                    }
                };

                [Fact]
                void SetupIsRunOnFirstMoveNextOnly()
                {
                    int setupCounter = 0;
                    Action setup = () => { setupCounter++; };
                    Karass testKarass = new Karass(new[] {setup}, new Action[0], Frames);

                    KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
                    actionRunner.MoveNext();
                    _output.WriteLine(setupCounter.ToString());
                    Assert.True(setupCounter == 1);
                    actionRunner.MoveNext();
                    _output.WriteLine(setupCounter.ToString());
                    Assert.True(setupCounter == 1);
                    
                }
            }
        }
    }
}