using System;
using KanKanCore;
using KanKanCore.Karass;

namespace KanKanTest.Mocks.UAction
{
    public class KarassStringDummy : Karass<string>
    {
        public override void Setup() {}

        public override void Teardown() {}

        public override Func<string,bool>[] Frames { get; }

        public KarassStringDummy() : base(string.Empty)
        {
            Frames = new Func<string,bool>[0];
        }
    }
}