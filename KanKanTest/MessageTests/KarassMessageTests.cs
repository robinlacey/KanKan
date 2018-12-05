using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using Xunit;

namespace KanKanTest.MessageTests
{
    public class KarassMessageTests
    {
        [Fact]
        public void UActionMessageGetMessageReturnsLastSetMessage()
        {
            IKarassMessage karassMessage = new KarassMessage();

            karassMessage.SetMessage("Cat");
            Assert.True(karassMessage.Message == "Cat");

            karassMessage.SetMessage("Dog");
            Assert.True(karassMessage.Message == "Dog");
        }

        [Fact]
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