using KanKanCore.Karass.Interface;

namespace KanKanTest.Mocks.UAction
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