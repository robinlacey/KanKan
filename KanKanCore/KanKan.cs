using System.Collections;
using System.Linq;
using KanKanCore.Karass.Interface;

// Kan-Kan: The instrument that brings individuals into their karass

// TODO : 
// Karass as a monoid
// Combine Setup and Teardown
 // (Action[])
// Combine inputted Params ? (later)
// Combine Functions 
// 
//  K+K = K (Associative)
// Neutral Element  = no functions
// Multiple Karass
//     Chain
//     Run at same time (
// KanKan.Run(Karass) - just one
// KanKan.Run(Karass1 + Karass2 + Karass3) - all at the same time
// KanKan.Run(Karass[]) Run one after another
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
            if (_frame == 0)
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
            if (Karass.Frames.Count > 0)
            {
                return Karass.Frames[0][_frame].Invoke(_message.Message);
            }

            return true;
           
        }

        private bool ShouldClearMessage()
        {
            return _messageFrame < _frame;
        }

        private bool IsLastFrame()
        {
            if (Karass.Frames.Count > 0)
            {
                return _frame > Karass.Frames[0].Length - 1;
            }
            return true;
        }
    }
}