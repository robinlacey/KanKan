using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.KarassFrame;

namespace KanKanTest.Mocks.KarassMocks
{
    public class KarassFramesStub : KanKanCore.Karass.Karass
    {
        public KarassFramesStub(List<FrameRequest[]> frames, IDependencies dependencies, IFrameFactory framesFactory) : base(
            dependencies, 
            framesFactory, 
            new List<List<Action>>(),
            new List<List<Action>>(), frames)
        {
        }
    }
}