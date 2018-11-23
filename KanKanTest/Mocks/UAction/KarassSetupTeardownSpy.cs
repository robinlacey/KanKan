using System;
using KanKanCore;
using KanKanCore.Karass;

namespace KanKanTest.Mocks.UAction
{
    internal class KarassSetupTeardownSpy : Karass<string>
    {
        public int SetupCounter { get; private set; }
        public int TeardownCounter { get; private set; }

        public override void Setup()
        {
            SetupCounter++;
        }

        public override void Teardown()
        {
            TeardownCounter++;
        }

        public sealed override Func<string,bool>[] Frames { get; }

        public KarassSetupTeardownSpy(int frames = 0) : base(String.Empty)
        {
            Frames = new Func<string,bool>[frames];
            for (int i = 0; i < frames; i++)
            {
                Frames[i] = (s) => true;
            }
        }
    }
}