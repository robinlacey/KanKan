using System.Collections;
using KanKanCore;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.UAction;
using Xunit;


namespace KanKanTest
{
    public class KanKanCurrentTests
    {
        [Fact]
        public void GivenNoFramesCurrentReturnsNull()
        {
            IEnumerator uActionRunner = new KanKan(new KarassStringDummy(),new KarassMessageDummy());
            Assert.Null(uActionRunner.Current); 
        }
        
        [Fact]
        public void GivenOneFrameCurrentIsNotNull()
        {
            IKarass karass = new KarassNumberOfFramesStub(1);
            Assert.True(karass.Frames.Length == 1);
            KanKan kanKan = new KanKan(karass,new KarassMessageDummy());
            Assert.NotNull(kanKan.Current); 
            
        }
         
        [Fact]
        public void GivenANumberOfFramesCurrentReturnsCorrectObject()
        {
            int frames = 10;
            IKarass karass = new KarassNumberOfFramesStub(frames);
            Assert.True(karass.Frames.Length == frames);
            KanKan kanKan = new KanKan(karass,new KarassMessageDummy());
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(kanKan.Current,karass.Frames[i]); 
                kanKan.MoveNext();
            }
        }
        
        [Fact]
        public void GivenANumberOfFramesCurrentReturnsNull()
        {
            IEnumerator uActionRunner = new KanKan(new KarassStringDummy(),new KarassMessageDummy());
            Assert.Null(uActionRunner.Current); 
        }
    }
}