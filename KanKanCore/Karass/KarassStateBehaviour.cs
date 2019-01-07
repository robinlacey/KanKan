using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Struct;

namespace KanKanCore.Karass
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

        public static bool IsEmptyKarass(IKarass karass)
        {
            return karass.FramesCollection.Count == 0;
        }


        public static int AddFrame(UniqueKarassFrameRequestID frameRequestID, Dictionary<UniqueKarassFrameRequestID, int>currentFrames)
        {
            return currentFrames[frameRequestID] += 1;
        }


        private static void TeardownKarass(int index, ref int lastFrameCount, out bool allFramesTornDown, IKarassState karassState)
        {
            karassState.Karass.Teardown(index);
            karassState.Complete[index] = true;
            lastFrameCount++;
            allFramesTornDown = lastFrameCount == karassState.Karass.FramesCollection.Count;
        }

        
        public static void InvokeTeardownActionsIfLastFrame(
            int index, 
            int currentFrame,
            ref int lastFrameCount, 
            out bool shouldComplete,
            IKarassState karassState)
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

        private static bool IsLastFrame(int currentFrame, IReadOnlyCollection<FrameRequest> allFrames, IKarass karass)
        {
            if (karass.FramesCollection.Count > 0)
            {
                return currentFrame > allFrames.Count - 1;
            }

            return true;
        }
        
        
        public static void MoveNextFramesToLastFrames(IKarassState karassState)
        {
            karassState.LastFrames.Clear();
            karassState.NextFrames.ForEach(f => karassState.LastFrames.Add(f));
        }
    }
}