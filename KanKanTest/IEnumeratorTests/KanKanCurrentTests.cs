using System.Collections;
using KanKanCore;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.Karass;
using KanKanTest.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.IEnumeratorTests
{
    public class KanKanCurrentTests
    {
        [Test]
        public void GivenNoFramesCurrentReturnsNotNull()
        {
            IEnumerator uActionRunner = new KanKan(new KarassDummy(), new KarassMessageDummy());
            Assert.NotNull(uActionRunner.Current);
        }

        [Test]
        public void GivenOneFrameCurrentIsNotNull()
        {
            IKarass karass = new KarassNumberOfFramesStub(1);
            Assert.True(karass.FramesCollection.Count == 1);
            KanKan kanKan = new KanKan(karass, new KarassMessageDummy());
            Assert.NotNull(kanKan.Current);
        }

        [Test]
        public void CurrentFramesReturnsNextFrames()
        {
            int frames = 10;
            IKarass karass = new KarassNumberOfFramesStub(frames);
            Assert.True(karass.FramesCollection[0].Length == frames);
            KanKan kanKan = new KanKan(karass, new KarassMessageDummy());
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(kanKan.Current, kanKan.CurrentState.NextFrames);
                kanKan.MoveNext();
            }
        }
    }
}