using System;
using KanKanCore;
using KanKanCore.Karass;

namespace KanKanTest.Mocks.UAction
{
    public class KarassNumberOfFramesStub:Karass<object>
    {
        public KarassNumberOfFramesStub(int frameCount ) : base(new object())
        {
            Frames = new Func<string,bool>[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                Frames[i] = (s) => true;
            }
        }
        
        public override void Setup() {}

        public override void Teardown() {}

        public sealed override Func<string,bool>[] Frames { get; }
    }
}