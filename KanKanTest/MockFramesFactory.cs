using System;
using KanKanCore.Factories;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Frame.SimpleKarassFrame;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.KarassFrame.FrameStruct;

namespace KanKanTest
{
    public class MockFramesFactory
    {
        private int _ticker;
        private int _frameStructDummyTicker;
        private readonly IFrameFactory _frameFactory;
        private readonly IDependencies _dependencies;

        public MockFramesFactory(IFrameFactory frameFactory, IDependencies dependencies)
        {
            _frameFactory = frameFactory;
            _dependencies = dependencies;
        }

        public FrameRequest GetValidFrameRequest(Func<string, bool> func, string testMessage)
        {
           

            if (_frameStructDummyTicker == 0)
            {
                FrameStructDummyOne frameActionData = new FrameStructDummyOne()
                {
                    Test = testMessage
                };
                IKarassFrame<FrameStructDummyOne> frameAction =
                    new SimpleKarassFrame<FrameStructDummyOne>(func, frameActionData);
                _dependencies.Register<IKarassFrame<FrameStructDummyOne>>(() => frameAction);
                _frameFactory.RegisterRoute<FrameStructDummyOne, IKarassFrame<FrameStructDummyOne>>();

                _frameStructDummyTicker++;
                return new FrameRequest(frameActionData);
            }
            else
            {
                FrameStructDummyTwo frameActionData = new FrameStructDummyTwo()
                {
                    Test = testMessage
                };
                IKarassFrame<FrameStructDummyTwo> frameAction =
                    new SimpleKarassFrame<FrameStructDummyTwo>(func, frameActionData);
                _dependencies.Register<IKarassFrame<FrameStructDummyTwo>>(() => frameAction);
                _frameFactory.RegisterRoute<FrameStructDummyTwo, IKarassFrame<FrameStructDummyTwo>>();

                _frameStructDummyTicker++;
                return new FrameRequest(frameActionData);
            }
        }

        public FrameRequest GetInvalidFrameRequest()
        {
            _ticker++;
            return new FrameRequest(new Random(_ticker).Next(0, int.MaxValue).ToString());
        }
    }
}