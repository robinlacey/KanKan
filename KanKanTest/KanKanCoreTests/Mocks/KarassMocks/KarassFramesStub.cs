using System;
using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassMocks
{
    public class KarassFramesStub : KanKanCore.Karass.Karass
    {
        public KarassFramesStub(List<FrameRequest[]> frames, IDependencies dependencies, IFrameFactory framesFactory) : base(
            new List<List<Action>>(),
            new List<List<Action>>(), frames)
        {
        }
    }
}