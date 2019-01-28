using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassMocks
{
    public class KarassNumberOfFramesStub:KanKanCore.Karass.Karass
    {
        private static List<FrameRequest[]> GetFakeFrames(int frameCount)
        {
            MockFramesFactory mockFramesFactory = new MockFramesFactory(new FrameFactoryDummy());
            FrameRequest[] frames = new FrameRequest[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = mockFramesFactory.GetInvalidFrameRequest();
            }
           
            return new List<FrameRequest[]>(){frames};
        }

        public KarassNumberOfFramesStub(int framesCount) : base(
            new List<List<Action>>(), 
            new List<List<Action>>(),
            GetFakeFrames(framesCount))
        {
           
        }
    }
}