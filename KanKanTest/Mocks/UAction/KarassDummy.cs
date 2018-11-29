using System;
using System.Collections.Generic;
using KanKanCore.Karass;

namespace KanKanTest.Mocks.UAction
{
    public class KarassDummy : Karass
    {
        public KarassDummy() : base(new Action[] {}, new Action[] {}, new List<Func<string, bool>[]>())
        {
            
        }
    }
}