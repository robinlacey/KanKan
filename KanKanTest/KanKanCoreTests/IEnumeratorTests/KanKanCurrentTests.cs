using System;
using System.Collections;
using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.IEnumeratorTests
{
    public class KanKanCurrentTests
    {
        [Test]
        public void GivenNoFramesCurrentReturnsNotNull()
        {
            IEnumerator kankan = new KanKan(new KarassDummy(), new FrameFactory(new KarassDependencies()));
            Assert.NotNull(kankan.Current);
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
          
            IKarass karass = karassFactory.Get(
                new List<Action>(), 
                new List<Action>(), new 
                    List<FrameRequest[]>
                    { new FrameRequest[] {}
            });
           
            KanKan kanKan = new KanKan(karass,frameFactory);
            IKanKanCurrentState karassState = kanKan.Current as IKanKanCurrentState;
            Assert.IsNotNull(karassState);
        }
    }
}