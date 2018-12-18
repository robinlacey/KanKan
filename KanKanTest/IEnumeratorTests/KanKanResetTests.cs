using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.UAction;
using NUnit.Framework;

namespace KanKanTest.IEnumeratorTests
{
    public class KanKanResetTests
    {
        private int _timesFirstFrameRun;
        private int _timesSecondFrameRun;

        [SetUp]
        public void Setup()
        {
            _timesFirstFrameRun = 0;
            _timesSecondFrameRun = 0;
        }
        [Test]
        public void GivenNoMoveNextFirstFrameReturnedOnReset()
        {
            KarassFramesStub karass = new KarassFramesStub(new List<Func<string, bool>[]>
                {new Func<string, bool>[] {FirstFrameSpy}});
            KanKan kanKan = new KanKan(karass, new KarassMessageDummy());

            kanKan.MoveNext();
            kanKan.Reset();
            kanKan.MoveNext();
            Assert.True(_timesFirstFrameRun == 2);
        }

        [Test]
        public void GivenMoveNextCalledFirstFrameReturnedOnReset()
        {
            KarassFramesStub karass = new KarassFramesStub(new List<Func<string, bool>[]>()
                {new Func<string, bool>[] {FirstFrameSpy, SecondFrameSpy}});
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