using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.Karass;
using KanKanTest.Mocks.KarassFrame;
using NUnit.Framework;

namespace KanKanTest.SetupTeardownTests.Setup
{
    public class KanKanSetupTests
    {
        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy(),new FrameFactoryDummy());

        [Test]
        public void SetupIsRunOnMoveNext()
        {
            int setupCounter = 0;
            Action setup = () => { setupCounter++; };
            Karass testKarass = KarassFactory.Get(CreateActionListWith(setup), new List<List<Action>>(),
                new List<FrameRequest[]>());

            KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
            actionRunner.MoveNext();
            Assert.True(setupCounter > 0);
        }

        public class GivenMultipleFrames
        {
            
            private IDependencies _dependencies;
            private IFrameFactory _frameFactory;
            private MockFramesFactory _mockFramesFactory;

            [SetUp]
            public void Setup()
            {
                _dependencies = new KarassDependencies();
                _frameFactory = new FrameFactory(_dependencies);
                _mockFramesFactory = new MockFramesFactory(_frameFactory,_dependencies);
            }

            
            bool FrameOne(string message) => true;
            bool FrameTwo(string message) => true;

            private List<FrameRequest[]> Frames => new List<FrameRequest[]>
            {
                new[]
                {
                    _mockFramesFactory.GetValidFrameRequest(FrameOne),
                    _mockFramesFactory.GetValidFrameRequest(FrameTwo),
                }
            };

            [Test]
            public void SetupIsRunOnFirstMoveNextOnly()
            {
                int setupCounter = 0;
                Action setup = () => { setupCounter++; };
                Karass testKarass = KarassFactory.Get(CreateActionListWith(setup), new List<List<Action>>(), Frames);

                KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
                actionRunner.MoveNext();
                Assert.True(setupCounter == 1);
                actionRunner.MoveNext();
                Assert.True(setupCounter == 1);
            }
        }

        private static List<List<Action>> CreateActionListWith(Action a) =>
            new List<List<Action>> {new List<Action> {a}};
    }
}