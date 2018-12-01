using System;
using System.Collections;
using System.Collections.Generic;
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
        public object Current => IsLastFrame() ? null : Karass.FramesCollection[_frame];

        public List<Func<string, bool>> NextFrames { get; } = new List<Func<string, bool>>();
        
        public IKarass Karass { get; }
        public int _frame;
        private int _messageFrame;
        private readonly IKarassMessage _message;
      
        public KanKan(IKarass karass,IKarassMessage karassMessage)
        {
            AddFirstFrames(karass.FramesCollection);
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
                // False will stop the Karass
                return false;
            }
            
            if (ShouldClearMessage())
            {
                _message.ClearMessage();
            }

         
            if ( ShouldProgressToNextFrame())
            {
                NextFrames.Clear();
               
                _frame++;
                
                if (IsLastFrame())
                {
                    Karass.Teardown();
                    return false;
                }
                else
                {
                    
                    NextFrames.Add(Karass.FramesCollection[0][_frame]);
                }
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
        
        
        private void AddFirstFrames(IEnumerable<Func<string, bool>[]> framesCollection)
        {
            foreach (var frames in framesCollection)
            {
                if (frames.Any())
                {
                    NextFrames.Add(frames[0]);
                }
            }
        }
        
        private bool ShouldProgressToNextFrame()
        {
            if (Karass.FramesCollection.Count > 0)
            {
                return Karass.FramesCollection[0][_frame].Invoke(_message.Message);
            }

            return true;
           
        }

        private bool ShouldClearMessage()
        {
            return _messageFrame < _frame;
        }

        private bool IsLastFrame()
        {
            if (Karass.FramesCollection.Count > 0)
            {
                return _frame > Karass.FramesCollection[0].Length - 1;
            }
            return true;
        }
    }
}