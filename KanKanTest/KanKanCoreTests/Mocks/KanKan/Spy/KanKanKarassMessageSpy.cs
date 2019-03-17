using System.Collections;
using System.Collections.Generic;
using KanKanCore.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy
{
    public class KanKanKarassMessageSpy:KanKanCore.KanKan.KanKan
    {
        public KanKanKarassMessageSpy(IKarass karass, IFrameFactory frameFactory) : base(karass, frameFactory)
        {
        }

        public KanKanKarassMessageSpy(IReadOnlyList<IKarass> karass, IFrameFactory frameFactory) : base(karass, frameFactory)
        {
        }

        public IKarassMessage GetKarassMessage() => KarassMessage;
    }
}