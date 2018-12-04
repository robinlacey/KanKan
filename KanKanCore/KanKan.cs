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
        public object Current => null; //IsLastFrame() ? null : Karass.FramesCollection[_frame];

        public List<Func<string, bool>> NextFrames { get; } = new List<Func<string, bool>>();
        private List<bool> _complete = new List<bool>();
        private IKarass Karass { get; }
       
        private readonly IKarassMessage _message;
      
        Dictionary<Func<string,bool>[],int> _currentFrames = new Dictionary<Func<string, bool>[], int>();
        public KanKan(IKarass karass,IKarassMessage karassMessage)
        {
            AddFirstFrames(karass.FramesCollection);
            
            Karass = karass;
            for (int i = 0; i < Karass.FramesCollection.Count; i++)
            {
                _currentFrames.Add(Karass.FramesCollection[i], 0);
                _complete.Add(false);
            }
            
            _message = karassMessage;
        }

        public bool MoveNext()
        {
            int lastFrameCount = 0;

            if (EmptyKarass())
            {
                InvokeAllSetupActions();
                InvokeAllTeardownActions();

                return false;
            }
            
            NextFrames.Clear();
            
            for (int i = 0; i < Karass.FramesCollection.Count; i++)
            {
                if (FrameSetAlreadyFinished(i))
                {
                    continue;
                }
                
                Func<string, bool>[] allFrames = Karass.FramesCollection[i];
                int currentFrame = _currentFrames[allFrames];
                
                InvokeSetupActionsOnFirstFrame(currentFrame, i);

                InvokeTeardownActionsOnLastFrame(i, currentFrame, allFrames, ref lastFrameCount, out bool shouldComplete);
                if (shouldComplete)
                {
                    return false;
                }
                
                bool goToNextFrame = InvokeCurrentFrame(i, currentFrame);

                if (!goToNextFrame) continue;
                
                currentFrame += 1;
                _currentFrames[allFrames] = currentFrame;
   
                InvokeTeardownActionsIfMovedOnToLastFrame(i, currentFrame, allFrames, ref lastFrameCount, out shouldComplete);
                if (shouldComplete)
                {
                    return false;
                }
            }
            _message.ClearMessage();
            
            return true;
        }
        
        private bool InvokeCurrentFrame(int index, int currentFrame)
        {
            return Karass.FramesCollection[index][currentFrame].Invoke(_message.Message);
        }

  
        private void TeardownKarass(int index, ref int lastFrameCount, out bool allFramesTornDown)
        {
            Karass.Teardown(index);
            _complete[index] = true;
            // False will stop the Karass
            lastFrameCount++;
            // Abort if all frames have been false

            allFramesTornDown = lastFrameCount == Karass.FramesCollection.Count;
        }

        private bool InvokeTeardownActionsOnLastFrame(int index, int currentFrame, Func<string, bool>[] allFrames, ref int lastFrameCount, out bool shouldComplete)
        {
            shouldComplete = false;
            if (IsLastFrame(currentFrame, allFrames))
            {
                TeardownKarass(index, ref lastFrameCount, out shouldComplete);
              
            }
    
            return true;
        }
        
        private void InvokeTeardownActionsIfMovedOnToLastFrame(int index, int currentFrame, Func<string, bool>[] allFrames, ref int lastFrameCount, out bool shouldComplete)
        {
            shouldComplete = false;
            if (IsLastFrame(currentFrame, allFrames))
            {
                TeardownKarass(index, ref lastFrameCount, out shouldComplete);
            }
            else
            {
                NextFrames.Add(Karass.FramesCollection[index][currentFrame]);
            }
        }
        

        private void InvokeSetupActionsOnFirstFrame(int currentFrame, int index)
        {
            if (currentFrame == 0)
            {
                Karass.Setup(index);
            }
        }

        private bool FrameSetAlreadyFinished(int index)
        {
            return _complete[index];
        }

        private void InvokeAllTeardownActions()
        {
            for (int i = 0; i < Karass.TeardownActions.Count; i++)
            {
                Karass.Teardown(i);
            }
        }

        private void InvokeAllSetupActions()
        {
            for (int i = 0; i < Karass.SetupActions.Count; i++)
            {
                Karass.Setup(i);
            }
        }

        private bool EmptyKarass()
        {
            return Karass.FramesCollection.Count == 0;
        }

        public void Reset()
        {
        }

        public void SendMessage(string message)
        {
            _message.SetMessage(message);
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
        
        private bool IsLastFrame(int currentFrame,  Func<string, bool>[] allFrames )
        {
            if (Karass.FramesCollection.Count > 0)
            {
                return currentFrame > allFrames.Length - 1;
            }
            return true;
        }
    }
}