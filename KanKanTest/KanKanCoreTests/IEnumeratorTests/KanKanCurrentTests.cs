using System;
using System.Collections;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.IEnumeratorTests
{
    public class KanKanCurrentTests
    {
        [Test]
        public void GivenNoFramesCurrentReturnsNotNull()
        {
            IEnumerator ukankan = new KanKan(new KarassDummy(), new FrameFactory(new KarassDependencies()));
            Assert.NotNull(ukankan.Current);
        }

        [Test]
        public void GivenOneFrameCurrentIsNotNull()
        {
            IKarass karass = new KarassNumberOfFramesStub(1);
            Assert.True(karass.FramesCollection.Count == 1);
            KanKan kanKan = new KanKan(karass, new FrameFactory(new KarassDependencies()));
            Assert.NotNull(kanKan.Current);
        }

        [Test]
        public void CurrentFramesReturnsKarassState()
        {
            KarassFactory karassFactory = new KarassFactory();
            IDependencies dependencies = new KarassDependencies();
            FrameFactory frameFactory = new FrameFactory(dependencies);
            MockFramesFactory mockFramesFactory = new MockFramesFactory(frameFactory);
          
            IKarass karass = karassFactory.Get(
                new List<Action>(), 
                new List<Action>(), new 
                    List<FrameRequest[]>
                    { new FrameRequest[] {}
            });
           
            KanKan kanKan = new KanKan(karass,frameFactory);
            KarassState karassState = kanKan.Current as KarassState;
            Assert.IsNotNull(karassState);
        }
    }
}