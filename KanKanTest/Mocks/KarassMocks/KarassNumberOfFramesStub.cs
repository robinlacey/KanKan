using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanTest.Factories;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.KarassFrame;

namespace KanKanTest.Mocks.KarassMocks
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