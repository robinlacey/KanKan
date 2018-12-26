using KanKanCore.Interface;
using KanKanCore.Karass.Message;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.MessageTests
{
    public class KarassMessageTests
    {
        [Test]
        public void UActionMessageGetMessageReturnsLastSetMessage()
        {
            IKarassMessage karassMessage = new KarassMessage();

            karassMessage.SetMessage("Cat");
            Assert.True(karassMessage.Message == "Cat");

            karassMessage.SetMessage("Dog");
            Assert.True(karassMessage.Message == "Dog");
        }

        [Test]
        public void UActionMessageClearMessageSetsMessageToStringEmpty()
        {
            IKarassMessage karassMessage = new KarassMessage();

            karassMessage.SetMessage("Dog");
            karassMessage.ClearMessage();
            Assert.True(karassMessage.Message == string.Empty);

            karassMessage.SetMessage("Cat");
            karassMessage.ClearMessage();
            Assert.True(karassMessage.Message == string.Empty);
        }
    }
}