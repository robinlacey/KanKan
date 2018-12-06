using System;
using System.Collections.Generic;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;

namespace KanKanTest.Mocks.UAction
{
    public class KarassFramesStub : Karass
    {
        public KarassFramesStub(List<Func<string, bool>[]> frames) : base(new DependenciesDummy(), new List<List<Action>>(),
            new List<List<Action>>(), frames)
        {
        }
    }
}