using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

// Karass: "We Bokononists believe that humanity is organized into teams, teams that do God's Will without ever discovering what they are doing. Such a team is called a karass by Bokonon"
// (Cat's Cradle - Kurt Vonnegut)

namespace KanKanCore.Karass
{
    public class Karass : IKarass
    {
        public List<List<Action>> SetupActions { get; }
        public List<List<Action>> TeardownActions { get; }
        public List<FrameRequest[]> FramesCollection { get; }
        public string ID { get; }
        public Karass(List<List<Action>> setup, List<List<Action>> teardown,
            List<FrameRequest[]> framesCollection)
        {
            
            ID = Guid.NewGuid().ToString();
            FramesCollection = framesCollection;
            SetupActions = setup;
            TeardownActions = teardown;
        }

        public static Karass operator +(Karass karassOne, Karass karassTwo)
        {
            return new Karass(
            karassOne.SetupActions.Concat(karassTwo.SetupActions).ToList(),
            karassOne.TeardownActions.Concat(karassTwo.TeardownActions).ToList(),
            new List<FrameRequest[]> (karassOne.FramesCollection.Concat(karassTwo.FramesCollection)));
        }

  

        public void Setup(int index)
        {
            if (SetupActions == null || SetupActions.Count - 1 < index)
            {
                return;
            }

            SetupActions[index].ForEach(setup => setup.Invoke());
        }

        public void Teardown(int index)
        {
            if (TeardownActions == null || TeardownActions.Count - 1 < index)
            {
                return;
            }

            TeardownActions[index].ForEach(teardown => teardown.Invoke());
        }
    }
}