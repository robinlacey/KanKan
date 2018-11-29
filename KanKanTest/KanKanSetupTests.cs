using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Karass;
using KanKanTest.Mocks.UAction;
using Xunit;
namespace KanKanTest
{
    public class KanKanSetupTests
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
        
        public class GivenMultipleFrames
        {
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
                Assert.True(setupCounter == 1);
                actionRunner.MoveNext();
                Assert.True(setupCounter == 1);
                    
            }
        }
    }
}