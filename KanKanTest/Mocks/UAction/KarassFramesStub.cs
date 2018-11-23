using System;
using KanKanCore;
using KanKanCore.Karass;

namespace KanKanTest.Mocks.UAction
{
    public class KarassFramesStub : Karass<object>
    {
        public KarassFramesStub(Func<string,bool>[] frames ) : base(new object())
        {
            Frames = frames;
        }

        public override void Setup() {}

        public override void Teardown() {}

        public override Func<string,bool>[] Frames { get; }
    }
}