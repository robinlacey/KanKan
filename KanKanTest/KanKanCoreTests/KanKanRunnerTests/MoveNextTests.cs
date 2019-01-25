using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class MoveNextTests
    {
        public class GivenKanKanInConstructor
        {
            [Test]
            public void TheRunKanKanMoveNextByDefault()
            {
                KanKanCallCountSpy kanKanCallCountSpy = new KanKanCallCountSpy();
                IKanKanRunner kanKanRunner = new KanKanSingleRunner(kanKanCallCountSpy, string.Empty);
                kanKanRunner.MoveNext();
                Assert.True(kanKanCallCountSpy.MoveNextCallCount == 1);
            }
        }
    }
}