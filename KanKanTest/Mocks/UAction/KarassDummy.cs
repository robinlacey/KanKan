using System;
using System.Collections.Generic;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;

namespace KanKanTest.Mocks.UAction
{
    public class KarassDummy : Karass
    {
        public KarassDummy() : base(new DependenciesDummy(),new List<List<Action>>(), new List<List<Action>>(),
            new List<Func<string, bool>[]>())
        {
        }
    }
}