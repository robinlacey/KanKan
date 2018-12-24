using KanKanCore;
using KanKanCore.Karass.Interface;
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
       
    }
}