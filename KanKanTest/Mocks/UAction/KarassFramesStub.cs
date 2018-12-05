using System;
using System.Collections.Generic;
using KanKanCore.Karass;

namespace KanKanTest.Mocks.UAction
{
    public class KarassFramesStub : Karass
    {
        public KarassFramesStub(List<Func<string, bool>[]> frames) : base(new List<List<Action>>(),
            new List<List<Action>>(), frames)
        {
        }
    }
}