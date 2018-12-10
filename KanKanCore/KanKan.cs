using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Interface;

namespace KanKanCore
{
    public class KanKan : IEnumerator
    {
        public object Current => NextFrames;
        public List<Func<string, bool>> NextFrames { get; private set; } = new List<Func<string, bool>>();

        private Dictionary<Func<string, bool>[], int> _currentFrames = new Dictionary<Func<string, bool>[], int>();
        private readonly List<bool> _complete = new List<bool>();
        private IKarass Karass { get; }
        private readonly IKarassMessage _message;


        public KanKan(IKarass karass, IKarassMessage karassMessage)
        {
            Karass = karass;
            _message = karassMessage;

            AddFirstFrames(Karass.FramesCollection);

            for (int i = 0; i < Karass.FramesCollection.Count; i++)
            {
                _currentFrames.Add(Karass.FramesCollection[i], 0);
                _complete.Add(false);
            }
        }

        public KanKan(IKarass[] karass, IKarassMessage karassMessage)
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

                InvokeSetupActionsOnFirstFrame(_currentFrames[Karass.FramesCollection[i]], i);

                if (!InvokeCurrentFrame(i, _currentFrames[Karass.FramesCollection[i]])) continue;

                InvokeTeardownActionsIfLastFrame(i, AddFrame(Karass.FramesCollection[i]), Karass.FramesCollection[i],
                    ref lastFrameCount,
                    out bool shouldComplete);

                if (shouldComplete)
                {
                    return false;
                }
            }

            _message.ClearMessage();

            return true;
        }


        public void Reset()
        {
            NextFrames = new List<Func<string, bool>>();
            _currentFrames = new Dictionary<Func<string, bool>[], int>();
            _complete.Clear();

            AddFirstFrames(Karass.FramesCollection);

            for (int i = 0; i < Karass.FramesCollection.Count; i++)
            {
                _currentFrames.Add(Karass.FramesCollection[i], 0);
                _complete.Add(false);
            }
        }


        private int AddFrame(Func<string, bool>[] allFrames)
        {
            return _currentFrames[allFrames] += 1;
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


        private void InvokeTeardownActionsIfLastFrame(int index, int currentFrame,
            Func<string, bool>[] allFrames, ref int lastFrameCount, out bool shouldComplete)
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


        private bool IsLastFrame(int currentFrame, Func<string, bool>[] allFrames)
        {
            if (Karass.FramesCollection.Count > 0)
            {
                return currentFrame > allFrames.Length - 1;
            }

            return true;
        }
    }
}