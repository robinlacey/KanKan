using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests.Single
{
    public class MoveNextTests
    {
        public class GivenKanKanInConstructor
        {
            [Test]
            public void TheRunKanKanMoveNextByDefault()
            {
                KanKanCallCountSpy kanKanCallCountSpy = new KanKanCallCountSpy();
                IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(kanKanCallCountSpy, string.Empty);
                kanKanRunner.MoveNext();
                Assert.True(kanKanCallCountSpy.MoveNextCallCount == 1);
            }
        }
    }
}