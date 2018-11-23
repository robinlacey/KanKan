using System;
using KanKanCore;
using KanKanCore.Karass;

namespace KanKanTest.Mocks.UAction
{
    public class KarassDummy<T> : Karass<T>
    {
        public override void Setup() {}

        public override void Teardown() {}

        public override Func<string,bool>[] Frames => null;

        public KarassDummy(T param) : base(param) {}
    }
}