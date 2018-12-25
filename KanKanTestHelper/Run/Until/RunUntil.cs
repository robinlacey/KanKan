using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTestHelper.Exception;
using KanKanTestHelper.Interface;
using KanKanTestHelper.Run.CurrentState;

namespace KanKanTestHelper.Run.Until
{
    public class RunUntil:IRunUntil
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
                if (ShouldReturnKanKanState<T>(KanKan.LastFrames, payload))
                {
                    return new KanKanCurrentState
                    {
                        NextFrames = KanKan.NextFrames,
                        LastFrames = KanKan.LastFrames
                    };
                }
            }

            throw new NoValidRequestType();
        }

        public IKanKanCurrentState NextFrame<T>(T payload)
        {
            KanKan.Reset();
            while (KanKan.MoveNext())
            {
                
                if (ShouldReturnKanKanState<T>(KanKan.NextFrames, payload))
                {
                    return new KanKanCurrentState
                    {
                        NextFrames = KanKan.NextFrames,
                        LastFrames = KanKan.LastFrames
                    };
                }
            }

            throw new NoValidRequestType();
        }

        public static bool ShouldReturnKanKanState<T>(List<FrameRequest> frameRequests, T payload)
        {
            foreach (FrameRequest frameRequest in frameRequests)
            {
                if (frameRequest.RequestType != typeof(T)) { continue;}
                T requestObject = (T) frameRequest.RequestObject;
                if (!requestObject.Equals(payload)) { continue;}

                {
                    return true;
                }
            }

            return false;
        }
    }
}