using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Interface;

namespace KanKanCore
{
    public class KarassData
    {
        public List<Func<string, bool>> NextFrames { get; set; } = new List<Func<string, bool>>();

        public Dictionary<Func<string, bool>[], int> CurrentFrames = new Dictionary<Func<string, bool>[], int>();
        public readonly List<bool> Complete = new List<bool>();
        public IKarass Karass { get; }

        public KarassData(IKarass karass)
        {
            Karass = karass;
            AddFirstFrames(Karass.FramesCollection);

            for (int i = 0; i < Karass.FramesCollection.Count; i++)
            {
                CurrentFrames.Add(Karass.FramesCollection[i], 0);
                Complete.Add(false);
            }
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

        public bool FrameSetAlreadyFinished(int index)
        {
            return Complete[index];
        }

        public void InvokeAllTeardownActions()
        {
            for (int i = 0; i < Karass.TeardownActions.Count; i++)
            {
                Karass.Teardown(i);
            }
        }

        public void InvokeAllSetupActions()
        {
            for (int i = 0; i < Karass.SetupActions.Count; i++)
            {
                Karass.Setup(i);
            }
        }

        public bool EmptyKarass()
        {
            return Karass.FramesCollection.Count == 0;
        }


        public int AddFrame(Func<string, bool>[] allFrames)
        {
            return CurrentFrames[allFrames] += 1;
        }

        public bool InvokeCurrentFrame(int index, int currentFrame, IKarassMessage message)
        {
            return Karass.FramesCollection[index][currentFrame].Invoke(message.Message);
        }


        private void TeardownKarass(int index, ref int lastFrameCount, out bool allFramesTornDown)
        {
            Karass.Teardown(index);
            Complete[index] = true;
            // False will stop the Karass
            lastFrameCount++;
            // Abort if all frames have been false

            allFramesTornDown = lastFrameCount == Karass.FramesCollection.Count;
        }


        public void InvokeTeardownActionsIfLastFrame(int index, int currentFrame,
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


        public void InvokeSetupActionsOnFirstFrame(int currentFrame, int index)
        {
            if (currentFrame == 0)
            {
                Karass.Setup(index);
            }
        }

        private bool IsLastFrame(int currentFrame, Func<string, bool>[] allFrames)
        {
            if (Karass.FramesCollection.Count > 0)
            {
                return currentFrame > allFrames.Length - 1;
            }

            return true;
        }


        public void Reset()
        {
            NextFrames = new List<Func<string, bool>>();
            CurrentFrames = new Dictionary<Func<string, bool>[], int>();
            Complete.Clear();

            AddFirstFrames(Karass.FramesCollection);

            for (int j = 0; j < Karass.FramesCollection.Count; j++)
            {
                CurrentFrames.Add(Karass.FramesCollection[j], 0);
                Complete.Add(false);
            }
        }
    }
}