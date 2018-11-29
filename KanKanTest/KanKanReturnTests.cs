//using KanKanCore;
//using KanKanCore.Karass.Interface;
//using KanKanTest.Mocks.UAction;
//using Xunit;
//
//namespace KanKanTest
//{
//    public class KanKanReturnTests
//    {
//        [Fact]
//        void GivenNoFramesReturnFalse()
//        {
//            IKarass karass = new KarassStringDummy();
//            KanKan actionRunner = new KanKan(karass,new KarassMessageDummy());   
//            Assert.False( actionRunner.MoveNext());
//        }
//
//        [Fact]
//        void GivenOneFrameReturnTrueThenFalse()
//        {
//            IKarass karass = new KarassNumberOfFramesStub(1);
//            KanKan actionRunner = new KanKan(karass,new KarassMessageDummy());   
//            Assert.True( actionRunner.MoveNext());
//            Assert.False( actionRunner.MoveNext());
//        }  
//    }
//}