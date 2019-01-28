using KanKanCore.Interface;
using KanKanCore.KanKan;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKanRunner
{
    public class KanKanSequenceRunnerSpy:KanKanSequenceRunner
    {
        public int TotalKanKan => KanKans.Keys.Count;

        public KanKanSequenceRunnerSpy(IKanKan[] currentKanKan, string tag) : base(currentKanKan, tag)
        {
        }
    }
}