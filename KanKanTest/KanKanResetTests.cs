using System;
using KanKanCore;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest
{
    public class KanKanResetTests
    {
        private int _timesFirstFrameRun;
        private int _timesSecondFrameRun;
        
        [Fact]
        public void GivenNoMoveNextFirstFrameReturnedOnReset()
        {
            KarassFramesStub karass = new KarassFramesStub(new[] {(Func<string, bool>) FirstFrameSpy});
            KanKan kanKan = new KanKan(karass, new KarassMessage());
            
            
            kanKan.MoveNext();
            kanKan.Reset();
            kanKan.MoveNext();
            
            Assert.True(_timesFirstFrameRun == 2);
        }
        
        [Fact]
        public void GivenMoveNextCalledFirstFrameReturnedOnReset()
        {
            KarassFramesStub karass = new KarassFramesStub(new[] {(Func<string, bool>) FirstFrameSpy,SecondFrameSpy});
            KanKan kanKan = new KanKan(karass, new KarassMessage());
         
            kanKan.MoveNext();
            kanKan.Reset();
            kanKan.MoveNext();
            
            Assert.True(_timesFirstFrameRun == 2);
            Assert.True(_timesSecondFrameRun == 0);
        }
        
        
        private bool FirstFrameSpy(string s)
        {
            _timesFirstFrameRun++;
            return true;
        }   
            
        private bool SecondFrameSpy(string s)
        {
            _timesSecondFrameRun++;
            return false;
        }
    }
}