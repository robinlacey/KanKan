using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Interface;

// Karass: team of individuals who do God's will without ever discovering what they are doing; every person belongs to one

namespace KanKanCore.Karass
{
    public class Karass : IKarass 
    {
        public void Setup(int index)
        {
            if (SetupActions == null || SetupActions.Count - 1 < index)
            {
                return;
            }
            SetupActions[index].ForEach(setup=>setup.Invoke());
        }

        public void Teardown(int index)
        {
            if (TeardownActions == null || TeardownActions.Count - 1 < index)
            {
                return;
            }   
            TeardownActions[index].ForEach(teardown=>teardown.Invoke());
        }

        public List<List<Action>> SetupActions { get;   set; }
        public List<List<Action>> TeardownActions { get;  set; }
        public List<Func<string, bool>[]> FramesCollection { get; protected set; }
       
        public Karass(List<List<Action>> setup, List<List<Action>> teardown, IEnumerable<Func<string, bool>[]> framesCollection)
        {
           
            FramesCollection =  (List<Func<string, bool>[]>) framesCollection;
  
            SetupActions = setup;
            TeardownActions = teardown;
        }

        public static Karass operator+ (Karass karassOne, Karass karassTwo) {
           
            
            return new Karass(
                karassOne.SetupActions.Concat(karassTwo.SetupActions).ToList(), 
                karassOne.TeardownActions.Concat(karassTwo.TeardownActions).ToList(), 
                new List<Func<string, bool>[]>(karassOne.FramesCollection.Concat(karassTwo.FramesCollection)) );
        }
    }
}