using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassMocks
{
    public class KarassDummy : KanKanCore.Karass.Karass
    {
        public KarassDummy() : base(
            new List<List<Action>>(), 
            new List<List<Action>>(),
            new List<FrameRequest[]>())
        {
        }
    }
}