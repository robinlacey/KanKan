using System;
using System.Collections.Generic;
using KanKanCore.Karass;

namespace KanKanTest.Mocks.UAction
{
    public class KarassFramesStub : Karass
    {
        public KarassFramesStub(List<Func<string, bool>[]> frames) : base(new Action[0], new Action[0], frames)
        {
        }
    }
}