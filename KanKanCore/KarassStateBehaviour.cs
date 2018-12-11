using System;
using System.Collections.Generic;
using KanKanCore.Karass.Interface;

namespace KanKanCore
{
    public static class KarassStateBehaviour
    {
        public static bool FrameSetAlreadyFinished(int index, List<bool> complete)
        {
            return complete[index];
        }

        public static void InvokeAllTeardownActions(IKarass karass)
        {
            for (int i = 0; i < karass.TeardownActions.Count; i++)
            {
                karass.Teardown(i);
            }
        }

        public static void InvokeAllSetupActions(IKarass karass)
        {
            for (int i = 0; i < karass.SetupActions.Count; i++)
            {
                karass.Setup(i);
            }
        }

        public static bool EmptyKarass(IKarass karass)
        {
            return karass.FramesCollection.Count == 0;
        }


        public static int AddFrame(Func<string, bool>[] allFrames, Dictionary<Func<string, bool>[], int> currentFrames)
        {
            return currentFrames[allFrames] += 1;
        }

        public static bool InvokeCurrentFrame(int index, int currentFrame, IKarassMessage message, IKarass karass )
        {
            return karass.FramesCollection[index][currentFrame].Invoke(message.Message);
        }


        private static void TeardownKarass(int index, ref int lastFrameCount, out bool allFramesTornDown, KarassState karassState)
        {
            karassState.Karass.Teardown(index);
            karassState.Complete[index] = true;
            // False will stop the Karass
            lastFrameCount++;
            // Abort if all frames have been false

            allFramesTornDown = lastFrameCount == karassState.Karass.FramesCollection.Count;
        }

        
        public static void InvokeTeardownActionsIfLastFrame(
            int index, 
            int currentFrame,
            ref int lastFrameCount, 
            out bool shouldComplete,
            KarassState karassState)
        {
            shouldComplete = false;
            
            if (IsLastFrame(currentFrame, karassState.Karass.FramesCollection[index],karassState.Karass))
            {
                TeardownKarass(index, ref lastFrameCount, out shouldComplete, karassState);
            }
            else
            {
                karassState.NextFrames.Add(karassState.Karass.FramesCollection[index][currentFrame]);
            }
        }

        public static void InvokeSetupActionsOnFirstFrame(int currentFrame, int index, IKarass karass)
        {
            if (currentFrame == 0)
            {
                karass.Setup(index);
            }
        }

        static bool IsLastFrame(int currentFrame, Func<string, bool>[] allFrames, IKarass karass)
        {
            if (karass.FramesCollection.Count > 0)
            {
                return currentFrame > allFrames.Length - 1;
            }

            return true;
        }
    }
}