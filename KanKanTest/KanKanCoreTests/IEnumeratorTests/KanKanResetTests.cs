using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.IEnumeratorTests
{
    public class KanKanResetTests
    {
        private int _timesFirstFrameRun;
        private int _timesSecondFrameRun;
        private  MockFramesFactory _mockFramesFactory;
        private IDependencies _dependencies;
        private IFrameFactory _frameFactory;
        [SetUp]
        public void Setup()
        {
            _dependencies = new KarassDependencies();
            _frameFactory = new FrameFactory(_dependencies);
            _mockFramesFactory = new MockFramesFactory(_frameFactory);
            _timesFirstFrameRun = 0;
            _timesSecondFrameRun = 0;
        }
        [Test]
        public void GivenNoMoveNextFirstFrameReturnedOnReset()
        {
            KarassFramesStub karass = new KarassFramesStub(
                new List<FrameRequest[]> {
                    new[]
                {
                    _mockFramesFactory.GetValidFrameRequest(FirstFrameSpy)
                }}, 
                _dependencies,
                _frameFactory
                );
            KanKan kanKan = new KanKan(karass, _frameFactory);

            kanKan.MoveNext();
            kanKan.Reset();
            kanKan.MoveNext();
            Assert.True(_timesFirstFrameRun == 2);
        }

        [Test]
        public void GivenMoveNextCalledFirstFrameReturnedOnReset()
        {
            KarassFramesStub karass = new KarassFramesStub(
                new List<FrameRequest[]>
                {
                    new []
                    {
                        _mockFramesFactory.GetValidFrameRequest(FirstFrameSpy),
                        _mockFramesFactory.GetValidFrameRequest(SecondFrameSpy)
                    }}, 
                _dependencies,
                _frameFactory);
            KanKan kanKan = new KanKan(karass,  _frameFactory);

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