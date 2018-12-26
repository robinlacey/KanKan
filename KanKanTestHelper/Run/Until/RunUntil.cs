using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;
using KanKanTestHelper.Exception;
using KanKanTestHelper.Interface;

namespace KanKanTestHelper.Run.Until
{
    public class RunUntil : IRunUntil
    {
        public IKanKan KanKan { get; }

        public RunUntil(IKanKan kanKan)
        {
            KanKan = kanKan;
        }

        public IKanKanCurrentState LastFrame<T>(T payload)
        {
            KanKan.Reset();
            while (KanKan.MoveNext())
            {
                if (ShouldReturnKanKanState<T>(KanKan.GetCurrentState().LastFrames, payload))
                {
                    return KanKan.GetCurrentState();
                }
            }

            if (CheckLastFramesBeforeKanKanCompletes(payload, out IKanKanCurrentState lastFrame)) {return lastFrame;}

            throw new NoValidRequestType();
        }

        public IKanKanCurrentState NextFrame<T>(T payload)
        {
            KanKan.Reset();
            
            if (CheckFirstFramesBeforeKanKanStarts(payload, out IKanKanCurrentState nextFrame)) { return nextFrame;}

            while (KanKan.MoveNext())
            {
                if (ShouldReturnKanKanState<T>(KanKan.GetCurrentState().NextFrames, payload))
                {
                    return KanKan.GetCurrentState();
                }
            }
            throw new NoValidRequestType();
        }

        private bool CheckFirstFramesBeforeKanKanStarts<T>(T payload, out IKanKanCurrentState nextFrame)
        {
            nextFrame = KanKan.GetCurrentState();
            return ShouldReturnKanKanState(KanKan.GetCurrentState().NextFrames, payload);
        }

        private bool CheckLastFramesBeforeKanKanCompletes<T>(T payload, out IKanKanCurrentState lastFrame)
        {
            lastFrame = KanKan.GetCurrentState();
            return ShouldReturnKanKanState(KanKan.GetCurrentState().LastFrames, payload);
        }

        public static bool ShouldReturnKanKanState<T>(List<FrameRequest> frameRequests, T payload)
        {
            foreach (FrameRequest frameRequest in frameRequests)
            {
                if (frameRequest.RequestType != typeof(T))
                {
                    continue;
                }

                T requestObject = (T) frameRequest.RequestObject;
                if (!requestObject.Equals(payload))
                {
                    continue;
                }
                return true;
            }
            return false;
        }
    }
}