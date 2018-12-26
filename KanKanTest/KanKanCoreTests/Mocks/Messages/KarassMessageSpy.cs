using KanKanCore.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.Messages
{
    public class KarassMessageSpy:IKarassMessage
    {
        public int SetMessageRunCount { get; private set; }
        public void SetMessage(string message)
        {
            SetMessageRunCount++;
        }
    
        public int ClearMessageRunCount { get; private set; }
        public void ClearMessage()
        {
            ClearMessageRunCount++;
        }

        public string Message { get; }
    }
}