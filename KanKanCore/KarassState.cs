using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Interface;

namespace KanKanCore
{
    public class KarassState
    {
        public List<Func<string, bool>> NextFrames { get; set; } = new List<Func<string, bool>>();
        public Dictionary<Func<string, bool>[], int> CurrentFrames = new Dictionary<Func<string, bool>[], int>();
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
            NextFrames = new List<Func<string, bool>>();
            CurrentFrames = new Dictionary<Func<string, bool>[], int>();
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
        
        private void AddFirstFrames(IEnumerable<Func<string, bool>[]> framesCollection, List<Func<string, bool>> nextFrames)
        {
            nextFrames.AddRange(from frames in framesCollection where frames.Any() select frames[0]);
        }
    }
}