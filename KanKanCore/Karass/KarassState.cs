using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Karass
{
    public class KarassState
    {
        public List<FrameRequest> NextFrames { get; set; } = new List<FrameRequest>();
        public Dictionary<FrameRequest[], int> CurrentFrames = new Dictionary<FrameRequest[], int>();
        public readonly List<bool> Complete = new List<bool>();
        public IKarass Karass { get; }

        public KarassState(IKarass karass)
        {
            Karass = karass;
                
            AddFirstFrames(Karass.FramesCollection, NextFrames);
            AddBaseValues();
        }

        public void Reset()
        {
            NextFrames = new List<FrameRequest>();
            CurrentFrames = new Dictionary<FrameRequest[], int>();
            Complete.Clear();

            AddFirstFrames(Karass.FramesCollection,NextFrames);

            foreach (var frames in Karass.FramesCollection)
            {
                CurrentFrames.Add(frames, 0);
                Complete.Add(false);
            }
        }
          
        private void AddBaseValues()
        {
            foreach (var frames in Karass.FramesCollection)
            {
                CurrentFrames.Add(frames, 0);
                Complete.Add(false);
            }
        }
        
        private void AddFirstFrames(IEnumerable<FrameRequest[]> framesCollection, List<FrameRequest> nextFrames)
        {
            nextFrames.AddRange(from frames in framesCollection where frames.Any() select frames[0]);
        }
    }
}