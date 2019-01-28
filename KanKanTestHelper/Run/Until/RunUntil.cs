using System.Collections.Generic;
using KanKanCore.Exception;
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
                if (CheckLastFrames(payload, out IKanKanCurrentState lastFrameOne)) {return lastFrameOne;}
            }

            if (CheckLastFrames(payload, out IKanKanCurrentState lastFrameTwo)) {return lastFrameTwo;}

            throw new NoValidRequestType();
        }

        public IKanKanCurrentState NextFrame<T>(T payload)
        {
            KanKan.Reset();
           
            if (CheckNextFrames(payload, out IKanKanCurrentState nextFrameState)) { return nextFrameState;}
            while (KanKan.MoveNext())
            {
                if (CheckNextFrames(payload, out nextFrameState)) { return nextFrameState;}
            }
            throw new NoValidRequestType();
        }

        public IKanKanCurrentState HasReceived(string message)
        {
            while (KanKan.MoveNext())
            {
                if (KanKan.Current.LastMessage == message)
                {
                    return KanKan.Current;
                }
                
            }
            
            if (KanKan.Current.LastMessage == message)
            {
                return KanKan.Current;
            }
            throw new MessageNotReceivedException();
        }

        public IKanKanCurrentState WillReceive(string message)
        {
            if (KanKan.Current.NextMessage == message)
            {
                return KanKan.Current;
            }
            while (KanKan.MoveNext())
            {
                if (KanKan.Current.NextMessage == message)
                {
                   return KanKan.Current;
                }
                
            }
           throw new MessageNotReceivedException();
        }

        private bool CheckNextFrames<T>(T payload, out IKanKanCurrentState nextFrame)
        {
            nextFrame = KanKan.Current;
            return ShouldReturnKanKanState(nextFrame.NextFrames, payload);
        }

        private bool CheckLastFrames<T>(T payload, out IKanKanCurrentState lastFrame)
        {
            lastFrame = KanKan.Current;
            return ShouldReturnKanKanState(lastFrame.LastFrames, payload);
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