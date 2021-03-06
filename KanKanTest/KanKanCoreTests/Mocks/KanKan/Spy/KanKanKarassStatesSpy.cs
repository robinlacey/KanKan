using System.Collections.Generic;
using KanKanCore.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy
{
    public class KanKanKarassStatesSpy:KanKanCore.KanKan.KanKan
    {

        public List<IKarassState> GetKarassStates() => AllKarassStates;
        public void SetKarassStates(List<IKarassState> states)
        {
            AllKarassStates = states;
        }

        public KanKanKarassStatesSpy(IKarass karass, IFrameFactory frameFactory) : base(karass, frameFactory)
        {
        }

        public KanKanKarassStatesSpy(IReadOnlyList<IKarass> karass, IFrameFactory frameFactory) : base(karass, frameFactory)
        {
        }
    }
}