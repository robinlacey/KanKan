using System;
using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Factories
{
    public class MockKarassFactory
    {
        private readonly MockFramesFactory _mockFramesFactory;
        public MockKarassFactory(MockFramesFactory mockFramesFactory)
        {
            _mockFramesFactory = mockFramesFactory;
        }

        public Karass GetKarassWithAmountOfData(int frameRequestArrayCount, int numberOfFrameRequests)
        {
          
            KarassFactory karassFactory = new KarassFactory();
            List<Action> setup = new List<Action>();
            List<Action> teardown  = new List<Action>();
            List<FrameRequest[]> frameRequests = new List<FrameRequest[]>();
          
            for (int i = 0; i < frameRequestArrayCount; i++)
            {
                frameRequests.Add(new FrameRequest[numberOfFrameRequests]);
                for (int j = 0; j <numberOfFrameRequests; j++)
                {
                    setup.Add(()=>{});
                    teardown.Add(()=>{});
                    frameRequests[i][j] = _mockFramesFactory.GetValidFrameRequest((s => true));
                   
                }
                Console.WriteLine("ADDING FRAME" + frameRequests[i].Length);
            }
            
            return karassFactory.Get(setup, teardown, frameRequests);
        } 
    }
}