using KanKanCore.Interface;
using KanKanCore.KanKan;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKanRunner
{
    public class KanKanSingleRunnerSpy:KanKanSingleRunner
    {
        public int TotalKanKan => KanKans.Keys.Count;
        
        public KanKanSingleRunnerSpy(IKanKan kanKan, string tag) : base(kanKan, tag)
        {
        }
    }
}