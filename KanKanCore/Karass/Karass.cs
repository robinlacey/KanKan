using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Interface;

// Karass: team of individuals who do God's will without ever discovering what they are doing; every person belongs to one

namespace KanKanCore.Karass
{
    public class Karass : IKarass 
    {
        public void Setup()
        {
            SetupActions.ToList().ForEach(setup=>setup.Invoke());
        }

        public void Teardown()
        {
            TeardownActions.ToList().ForEach(setup=>setup.Invoke());
        }

        public IEnumerable<Action> SetupActions { get;   set; }
        public IEnumerable<Action> TeardownActions { get;  set; }
        public List<Func<string, bool>[]> FramesCollection { get; protected set; }
       
        public Karass(IEnumerable<Action> setup, IEnumerable<Action>teardown, IEnumerable<Func<string, bool>[]> framesCollection)
        {
          
            FramesCollection =  (List<Func<string, bool>[]>) framesCollection;
  
            SetupActions = setup;
            TeardownActions = teardown;
        }


//
//        private void Example_SetFrames()
//        {
//            for (int i = 0; i < Frames.Count; i++)
//            {
//                if (Frames[i].Any())
//                {
//                    CurrentFrames.Add(i, 0);
//                }
//            }
//        }
//
//        // Something like .... 
//        void Example_Update()
//        {
//         // Go through frames
//            for (int i = 0; i < Frames.Count; i++)
//            {
//                // tick method
//                var func = Frames[i][CurrentFrames[i]];
//                bool goToNextFrame = func("message");
//                CurrentFrames[i] += goToNextFrame ? 1 : 0;
//                if (CurrentFrames[i] > Frames[i].Length - 1)
//                {
//                    // Remove
//                }
//            }
//        }
//        
        public static Karass operator+ (Karass karassOne, Karass karassTwo) {
           
            return new Karass(
                karassOne.SetupActions.Concat(karassTwo.SetupActions), 
                karassOne.TeardownActions.Concat(karassTwo.TeardownActions), 
                new List<Func<string, bool>[]>(karassOne.FramesCollection.Concat(karassTwo.FramesCollection)) );
        }
    }
}