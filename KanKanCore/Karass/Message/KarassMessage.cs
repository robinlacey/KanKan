using KanKanCore.Interface;

namespace KanKanCore.Karass.Message
{
    public class KarassMessage: IKarassMessage
    {
        public void SetMessage(string message)
        {
            Message = message;
        }

        public void ClearMessage()
        {
          Message = string.Empty;
        }

        public string Message { get; private set; } = string.Empty;
    }
}