using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.Factories;
using KanKanTest.Mocks.KarassFrame;
using NUnit.Framework;

namespace KanKanTest.SetupTeardownTests.Setup
{
    public class KanKanSetupTests
    {
        private static KarassFactory KarassFactory => new KarassFactory();

        [Test]
        public void SetupIsRunOnMoveNext()
        {
            int setupCounter = 0;
            Action setup = () => { setupCounter++; };
            Karass testKarass = KarassFactory.Get(CreateActionListWith(setup), new List<List<Action>>(),
                new List<FrameRequest[]>());

            KanKan kankan = new KanKan(testKarass, new FrameFactoryDummy());
            kankan.MoveNext();
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
                _mockFramesFactory = new MockFramesFactory(_frameFactory);
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

                KanKan kankan = new KanKan(testKarass, _frameFactory);
                kankan.MoveNext();
                Assert.True(setupCounter == 1);
                kankan.MoveNext();
                Assert.True(setupCounter == 1);
            }
        }

        private static List<List<Action>> CreateActionListWith(Action a) =>
            new List<List<Action>> {new List<Action> {a}};
    }
}