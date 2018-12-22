using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.MessageTests
{
    public class KanKanMessageTests
    {
        string _firstFrameMessage = string.Empty;
        string _secondFrameMessage = string.Empty;

        private MockFramesFactory _mockFramesFactory;
        private FrameRequest _firstFrameRequest;
        private FrameRequest _secondFrameRequest;
        private IDependencies _dependencies;
        private IFrameFactory _frameFactory;
        [SetUp]
        public void Setup()
        {
            _dependencies = new KarassDependencies();
            _frameFactory = new FrameFactory(_dependencies);
            _mockFramesFactory = new MockFramesFactory(_frameFactory, _dependencies);
            _firstFrameRequest = _mockFramesFactory.GetValidFrameRequest(FirstFrameSpy);
            _secondFrameRequest = _mockFramesFactory.GetValidFrameRequest(SecondFrameSpy);
        }
        [Test]
        public void UActionRunnerHasSendMessageMethod()
        {
            IKarassMessage karassMessage = new KarassMessage();
            Karass karass = new KarassDummy();
            KanKanCore.KanKan kanKan = new KanKanCore.KanKan(karass, karassMessage);
            kanKan.SendMessage("Cat");
        }

        [Test]
        public void UActionRunnerSendMessageSendsMessageToNextFrame()
        {
            IKarassMessage karassMessage = new KarassMessage();
            KarassFramesStub karass = new KarassFramesStub(
                new List<FrameRequest[]>
                {
                    new[]
                    {
                        _firstFrameRequest,
                        _secondFrameRequest
                    }
                },
                _dependencies, 
               _frameFactory);
            KanKanCore.KanKan kanKan = new KanKanCore.KanKan(karass, karassMessage);

            kanKan.SendMessage("Cat");
            kanKan.MoveNext();

            Assert.True(_firstFrameMessage == "Cat");

            kanKan.SendMessage("Doggo");
            kanKan.MoveNext();
            Assert.True(_secondFrameMessage == "Doggo");
        }

        [Test]
        public void MessagesOnlyLastForOneFrame()
        {
            IKarassMessage karassMessage = new KarassMessage();
            KarassFramesStub karass = new KarassFramesStub(
                new List<FrameRequest[]>
                {
                    new[]
                    {
                        _firstFrameRequest,
                        _secondFrameRequest
                    }
                },
                _dependencies, 
                _frameFactory);
            
            KanKanCore.KanKan kanKan = new KanKanCore.KanKan(karass, karassMessage);

            kanKan.SendMessage("Cat");
            kanKan.MoveNext();
            Assert.True(_firstFrameMessage == "Cat");

            kanKan.MoveNext();
            Assert.True(_secondFrameMessage == string.Empty);
        }

        private bool FirstFrameSpy(string s)
        {
            _firstFrameMessage = s;
            return true;
        }

        private bool SecondFrameSpy(string s)
        {
            _secondFrameMessage = s;
            return false;
        }
    }
}