using System;
using KanKanCore;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest
{
    public class KanKanFrameProgressionTests
    {
        private int _timesFirstFrameRun;
        private int _timesSecondFrameRun;

        [Fact]
        public void DoesNotContinueToNextFrameIfFalseIsReturned()
        {
            KarassFramesStub karass = new KarassFramesStub(new[] {(Func<string, bool>) FirstFrameSpy});
            KanKan kanKan = new KanKan(karass, new KarassMessage());

            kanKan.MoveNext();
            Assert.True(_timesFirstFrameRun == 1);

            kanKan.MoveNext();
            Assert.True(_timesFirstFrameRun == 2);
        }

        [Fact]
        public void WillContinueToNextFrameOnCorrectMessage()
        {
            IKarassMessage karassMessage = new KarassMessage();


            KarassFramesStub karass =
                new KarassFramesStub(new[] {(Func<string, bool>) FirstFrameSpy, SecondFrameSpy});
            KanKan kanKan = new KanKan(karass, karassMessage);

            int runCount = 100;
            for (int i = 0; i < runCount; i++)
            {
                kanKan.MoveNext();
                Assert.True(_timesFirstFrameRun == i + 1);
            }

            karassMessage.SetMessage("Doggos");
            kanKan.MoveNext();
            kanKan.MoveNext();

            Assert.True(_timesSecondFrameRun == 1);
        }
        
        private bool FirstFrameSpy(string s)
        {
            _timesFirstFrameRun++;
            return s == "Doggos";
        }

        private bool SecondFrameSpy(string s)
        {
            _timesSecondFrameRun++;
            return true;
        }

    }
}