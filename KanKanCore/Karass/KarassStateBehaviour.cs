using System.Collections.Generic;
using KanKanCore.Interface;

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


        public static void MoveNextFramesToLastFrames(IKarassState karassState)
        {
            karassState.LastFrames.Clear();
            karassState.NextFrames.ForEach(f => karassState.LastFrames.Add(f));
        }
    }
}