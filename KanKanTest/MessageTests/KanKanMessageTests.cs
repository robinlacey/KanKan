using System;
using System.Collections.Generic;
using KanKanCore.Karass;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.UAction;
using NUnit.Framework;

namespace KanKanTest.MessageTests
{
    public class KanKanMessageTests
    {
        string _firstFrameMessage = string.Empty;
        string _secondFrameMessage = string.Empty;

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
            KarassFramesStub karass = new KarassFramesStub(new List<Func<string, bool>[]>
                {new Func<string, bool>[] {FirstFrameSpy, SecondFrameSpy}});
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
            KarassFramesStub karass = new KarassFramesStub(new List<Func<string, bool>[]>
                {new Func<string, bool>[] {FirstFrameSpy, SecondFrameSpy}});
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