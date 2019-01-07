using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy
{
    public class KanKanKarassStatesSpy:KanKanCore.KanKan.KanKan
    {
        public KanKanKarassStatesSpy(IKarass karass, IFrameFactory frameFactory) : base(karass, frameFactory)
        {
        }

        public List<IKarassState> GetKarassStates() => AllKarassStates;
        public void SetKarassStates(List<IKarassState> states)
        {
            AllKarassStates = states;
        }
    }
}