using System;
using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassMocks
{
    public class KarassDummy : IKarass
    {
        public KarassDummy()
        {
            
            ID = Guid.NewGuid().ToString();
            FramesCollection = new List<FrameRequest[]>();
            SetupActions = new List<List<Action>>();
            TeardownActions = new List<List<Action>>();
        }

        
        public string ID { get; }
        public void Setup(int index)
        {
            throw new NotImplementedException();
        }

        public void Teardown(int index)
        {
            throw new NotImplementedException();
        }

        public List<List<Action>> SetupActions { get; }
        public List<List<Action>> TeardownActions { get; }
        public List<FrameRequest[]> FramesCollection { get; }
    }
}