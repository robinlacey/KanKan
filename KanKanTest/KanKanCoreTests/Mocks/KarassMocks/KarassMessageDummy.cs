using KanKanCore.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassMocks
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