using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
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
            Assert.AreSame(kankan.FrameFactory,frameFactory);
        }
        
        [Test]
        public void KanKanAssignKarassMessage()
        {
            IKarassMessage karassMessage = new KarassMessageDummy();
            IKanKan kankan = new KanKan(new KarassDummy(),new FrameFactoryDummy(),karassMessage);
            Assert.AreSame(kankan.KarassMessage,karassMessage);
        }
        
        [Test]
        public void KanKanCreatesKarassMessageIfNoneGiven()
        {
            IKanKan kankan = new KanKan(new KarassDummy(),new FrameFactoryDummy());
            Assert.NotNull(kankan.KarassMessage);
        }

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
                IKarass karass = karassFactory.Get(new List<Action>(), new List<Action>(),new FrameRequest[]
                {
                    frameRequest
                });
                KanKan kankan = new KanKan(karass,framesFactory);
                
                Assert.True(kankan.NextFrames.Contains(frameRequest));
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
                IKarass karass = karassFactory.Get(new List<Action>(), new List<Action>(),new[]
                {
                    frameRequestOne,
                    frameRequestTwo
                });
               
                KanKan kankan = new KanKan(karass,framesFactory);
                
                Assert.True(kankan.NextFrames.Contains(frameRequestOne));
                Assert.False(kankan.NextFrames.Contains(frameRequestTwo));
                Assert.False(kankan.LastFrames.Any());
                
                kankan.MoveNext();
                
                Assert.False(kankan.NextFrames.Contains(frameRequestOne));
                Assert.True(kankan.NextFrames.Contains(frameRequestTwo));
               
                Assert.True(kankan.LastFrames.Contains(frameRequestOne));
                Assert.False(kankan.LastFrames.Contains(frameRequestTwo));
                
                kankan.MoveNext();
                Assert.False(kankan.NextFrames.Any());
                Assert.False(kankan.LastFrames.Contains(frameRequestOne));
                Assert.True(kankan.LastFrames.Contains(frameRequestTwo));
            }
            
        }
       
    }
}