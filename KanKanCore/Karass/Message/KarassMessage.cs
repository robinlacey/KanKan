using KanKanCore.Interface;

namespace KanKanCore.Karass.Message
{
    public class KarassMessage: IKarassMessage
    {
        private string _message;
        public void SetMessage(string message)
        {
            _message = message;
        }

        public void ClearMessage()
        {
          _message = string.Empty;
        }

        public string Message => _message;
    }
}