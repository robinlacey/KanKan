using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Struct;

namespace KanKanCore.Karass
{
    public class KarassState : IKarassState
    {
        public string ID { get; }
        public List<FrameRequest> NextFrames { get; private set; } = new List<FrameRequest>();
        public List<FrameRequest> LastFrames { get; private set; } = new List<FrameRequest>();

        public Dictionary<UniqueKarassFrameRequestID, int> CurrentFrames { get; private set; } =
            new Dictionary<UniqueKarassFrameRequestID, int>();

        public List<bool> Complete { get; } = new List<bool>();
        public IKarass Karass { get; }

        public KarassState(IKarass karass)
        {
            Karass = karass;
            ID = Guid.NewGuid().ToString();
            if (Karass.FramesCollection == null || NextFrames == null)
            {
                return;
            }

            AddFirstFrames(Karass.FramesCollection, NextFrames);
            AddBaseValues();
        }

        public void Reset()
        {
            NextFrames = new List<FrameRequest>();
            LastFrames = new List<FrameRequest>();
            CurrentFrames = new Dictionary<UniqueKarassFrameRequestID, int>();
            Complete.Clear();

            AddFirstFrames(Karass.FramesCollection, NextFrames);

            for (int i = 0; i < Karass.FramesCollection.Count; i++)
            {
                CurrentFrames.Add(new UniqueKarassFrameRequestID(Karass.ID, i), 0);
                Complete.Add(false);
            }
        }

        private void AddBaseValues()
        {
            for (int i = 0; i < Karass.FramesCollection.Count; i++)
            {
                CurrentFrames.Add(new UniqueKarassFrameRequestID(Karass.ID, i), 0);
                Complete.Add(false);
            }
        }

        private void AddFirstFrames(IEnumerable<FrameRequest[]> framesCollection, List<FrameRequest> nextFrames)
        {
            nextFrames.AddRange(from frames in framesCollection where frames.Any() select frames[0]);
        }
    }
}