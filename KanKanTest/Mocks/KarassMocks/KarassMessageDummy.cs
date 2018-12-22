using KanKanCore.Karass.Interface;

namespace KanKanTest.Mocks.Karass
{
    public class KarassMessageDummy : IKarassMessage
    {
        public void SetMessage(string message)
        {
        }

        public void ClearMessage()
        {
        }

        public string Message { get; }
    }
}