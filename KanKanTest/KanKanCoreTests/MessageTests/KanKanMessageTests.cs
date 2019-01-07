using System;
using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Message;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.MessageTests
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
            _mockFramesFactory = new MockFramesFactory(_frameFactory);
            _firstFrameRequest = _mockFramesFactory.GetValidFrameRequest(FirstFrameSpy);
            _secondFrameRequest = _mockFramesFactory.GetValidFrameRequest(SecondFrameSpy);
        }
        [Test]
        public void KanKanHasSendMessageMethod()
        {
            Karass karass = new KarassDummy();
            KanKan kanKan = new KanKan(karass, new FrameFactory(new KarassDependencies()));
            kanKan.SendMessage("Cat");
        }

        [TestCase("Dog","Doggo")]
        [TestCase("Cat","Catto")]
        [TestCase("Cow","Cowwo")]
        [TestCase("Chicken","Chickenno")]
        public void KanKanSendMessageSendsMessageToNextFrame(string messageOne,string messageTwo)
        {
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
            KanKan kanKan = new KanKan(karass, _frameFactory);

            kanKan.SendMessage(messageOne);
            kanKan.MoveNext();

            Assert.True(_firstFrameMessage == messageOne);

            kanKan.SendMessage(messageTwo);
            kanKan.MoveNext();
            Assert.True(_secondFrameMessage == messageTwo);
        }

        [TestCase("Dog")]
        [TestCase("Cat")]
        [TestCase("Cow")]
        [TestCase("Chicken")]
        public void MessagesOnlyLastForOneFrame(string message)
        {
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
            
            KanKan kanKan = new KanKan(karass, _frameFactory);

            kanKan.SendMessage(message);
            kanKan.MoveNext();
            Assert.True(_firstFrameMessage == message);

            kanKan.MoveNext();
            Assert.True(_secondFrameMessage == string.Empty);
        }

        [TestCase("Dog")]
        [TestCase("Cat")]
        [TestCase("Cow")]
        [TestCase("Chicken")]
        
        public void MessageSendToMultipleKanKan(string message)
        {
            IKarassMessage karassMessage = new KarassMessage();
            string karassOneFrameMessage = string.Empty;
            
            bool KarassOneFrameSpy(string m)
            {
                karassOneFrameMessage = m;
                return true;
            }

            FrameRequest karassOneFrameRequest = _mockFramesFactory.GetValidFrameRequest(KarassOneFrameSpy);
            
            string karassTwoFrameMessage = string.Empty;
            
            bool KarassTwoFrameSpy(string m)
            {
                karassTwoFrameMessage = m;
                return true;
            }
            
            FrameRequest karassTwoFrameRequest = _mockFramesFactory.GetValidFrameRequest(KarassTwoFrameSpy);


            string karassThreeFrameMessage = string.Empty;
            
            bool KarassThreeFrameSpy(string m)
            {
                karassThreeFrameMessage = m;
                return true;
            }
            
            FrameRequest karassThreeFrameRequest = _mockFramesFactory.GetValidFrameRequest(KarassThreeFrameSpy);

            
            KarassFactory karassFactory = new KarassFactory();
             
            Karass karassOne = karassFactory.Get(new List<Action>(), new List<Action>(), new []{karassOneFrameRequest});
            Karass karassTwo = karassFactory.Get(new List<Action>(), new List<Action>(), new []{karassTwoFrameRequest});
            Karass karassThree = karassFactory.Get(new List<Action>(), new List<Action>(), new []{karassThreeFrameRequest});

            KanKan kanKanOne = new KanKan(karassOne, _frameFactory);
            kanKanOne.SetKarassMessage(karassMessage);
            KanKan kanKanTwo = new KanKan(karassTwo, _frameFactory);
            kanKanTwo.SetKarassMessage(karassMessage);
            KanKan kanKanThree = new KanKan(karassThree, _frameFactory);
            
            kanKanThree.SetKarassMessage(karassMessage);
            karassMessage.SetMessage(message);

            kanKanOne.MoveNext();
            kanKanTwo.MoveNext();
            kanKanThree.MoveNext();

            Assert.True(karassOneFrameMessage == message);
            Assert.True(karassTwoFrameMessage == message);
            Assert.True(karassThreeFrameMessage == message);
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