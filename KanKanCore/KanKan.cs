using System.Collections;
using KanKanCore.Karass.Interface;

// Kan-Kan: The instrument that brings individuals into their karass

namespace KanKanCore
{
    public class KanKan :IEnumerator
    {
        public object Current => IsLastFrame() ? null : Karass.Frames[_frame];

        private IKarass Karass { get; }
        private int _frame;
        private int _messageFrame;
        private readonly IKarassMessage _message;
      
        public KanKan(IKarass karass,IKarassMessage karassMessage)
        {
            Karass = karass;
            _message = karassMessage;
        }

        public bool MoveNext()
        {
            if (IsFirstFrame())
            {
                Karass.Setup();
            }

            if (IsLastFrame())
            {
                Karass.Teardown();
                return false;
            }

            if (ShouldClearMessage())
            {
                _message.ClearMessage();
            }

            if (ShouldProgressToNextFrame())
            {
                _frame++;
            }
            
            return true;
        }

        public void Reset()
        {
            _frame = 0;
            _messageFrame = 0;
        }

        public void SendMessage(string message)
        {
            _message.SetMessage(message);
            _messageFrame = _frame;
        }
        
        private bool ShouldProgressToNextFrame()
        {
            return Karass.Frames[_frame].Invoke(_message.Message);
        }

        private bool ShouldClearMessage()
        {
            return _messageFrame < _frame;
        }

        private bool IsLastFrame()
        {
            return _frame > Karass.Frames.Length - 1;
        }

        private bool IsFirstFrame()
        {
            return _frame == 0;
        }
    }
}