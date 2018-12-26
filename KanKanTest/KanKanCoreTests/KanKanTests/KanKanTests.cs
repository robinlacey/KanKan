using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanTests
{
    public class KanKanTests
    {
        [Test]
        public void KanKanAssignsFrameFactory()
        {
            IFrameFactory frameFactory = new FrameFactoryDummy();
            IKanKan kankan = new KanKan(new KarassDummy(), frameFactory);
            Assert.AreSame(kankan.GetCurrentState().FrameFactory, frameFactory);
        }

        [Test]
        public void KanKanAssignKarassMessage()
        {
            IKarassMessage karassMessage = new KarassMessageDummy();
            IKanKan kankan = new KanKan(new KarassDummy(), new FrameFactoryDummy(), karassMessage);
            Assert.AreSame(kankan.GetCurrentState().KarassMessage, karassMessage);
        }

        [Test]
        public void KanKanCreatesKarassMessageIfNoneGiven()
        {
            IKanKan kankan = new KanKan(new KarassDummy(), new FrameFactoryDummy());
            Assert.NotNull(kankan.GetCurrentState().KarassMessage);
        }

        public class GetKanKanCurrentStateTests
        {
            public class NextAndLastFrames
            {
                [Test]
                public void NextFramesShouldReturnKarassStateNextFrames()
                {
                    IDependencies dependencies = new KarassDependencies();
                    IFrameFactory framesFactory = new FrameFactory(dependencies);
                    MockFramesFactory _mockFramesFactory = new MockFramesFactory(framesFactory);

                    bool FrameSpy(string message) => false;
                    FrameRequest frameRequest = _mockFramesFactory.GetValidFrameRequest(FrameSpy);
                    KarassFactory karassFactory = new KarassFactory();
                    IKarass karass = karassFactory.Get(new List<Action>(), new List<Action>(), new FrameRequest[]
                    {
                        frameRequest
                    });
                    IKanKan kankan = new KanKan(karass, framesFactory);

                    Assert.True(kankan.GetCurrentState().NextFrames.Contains(frameRequest));
                }

                [Test]
                public void LastFramesShouldReturnPreviousFrames()
                {
                    IDependencies dependencies = new KarassDependencies();
                    IFrameFactory framesFactory = new FrameFactory(dependencies);
                    MockFramesFactory mockFramesFactory = new MockFramesFactory(framesFactory);

                    bool FrameSpyOne(string message) => true;
                    bool FrameSpyTwo(string message) => true;
                    FrameRequest frameRequestOne = mockFramesFactory.GetValidFrameRequest(FrameSpyOne);
                    FrameRequest frameRequestTwo = mockFramesFactory.GetValidFrameRequest(FrameSpyTwo);
                    KarassFactory karassFactory = new KarassFactory();
                    IKarass karass = karassFactory.Get(new List<Action>(), new List<Action>(), new[]
                    {
                        frameRequestOne,
                        frameRequestTwo
                    });

                    IKanKan kankan = new KanKan(karass, framesFactory);

                    Assert.True(kankan.GetCurrentState().NextFrames.Contains(frameRequestOne));
                    Assert.False(kankan.GetCurrentState().NextFrames.Contains(frameRequestTwo));
                    Assert.False(kankan.GetCurrentState().LastFrames.Any());

                    kankan.MoveNext();

                    Assert.False(kankan.GetCurrentState().NextFrames.Contains(frameRequestOne));
                    Assert.True(kankan.GetCurrentState().NextFrames.Contains(frameRequestTwo));

                    Assert.True(kankan.GetCurrentState().LastFrames.Contains(frameRequestOne));
                    Assert.False(kankan.GetCurrentState().LastFrames.Contains(frameRequestTwo));

                    kankan.MoveNext();
                    Assert.False(kankan.GetCurrentState().NextFrames.Any());
                    Assert.False(kankan.GetCurrentState().LastFrames.Contains(frameRequestOne));
                    Assert.True(kankan.GetCurrentState().LastFrames.Contains(frameRequestTwo));
                }
            }
        }
    }
}