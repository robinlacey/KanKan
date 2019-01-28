using KanKanCore.Interface;
using KanKanCore.KanKan;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKanRunner
{
    public class KanKanRunnerSpy<T>:KanKanRunner<T>
    {
        public int TotalKanKan => KanKans.Keys.Count;


        public KanKanRunnerSpy(T kankan, string tag) : base(kankan, tag)
        {
        }

        public override bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public override void Reset()
        {
            throw new System.NotImplementedException();
        }

        public override void Run(string tag)
        {
            throw new System.NotImplementedException();
        }
    }
}