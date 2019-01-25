using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class PauseKanKanTests
    {
        public class WhenPaused
        {
            [TestCase(1)]
            [TestCase(23)]
            public void ThenRunnerReturnsTrueAndKanKanMoveNextIsNotExecuted(int pauseFor)
            {
                KanKanCallCountSpy kanKanCallCountSpy = new KanKanCallCountSpy();
                IKanKanRunner kanKanRunner = new KanKanSingleRunner(kanKanCallCountSpy, "Scout");
                kanKanRunner.MoveNext();
                Assert.True(kanKanCallCountSpy.MoveNextCallCount == 1);
                kanKanRunner.Pause(true);
                for (int i = 0; i < pauseFor; i++)
                {
                    kanKanRunner.MoveNext();
                }

                Assert.True(kanKanCallCountSpy.MoveNextCallCount == 1);
            }
        }

        public class WhenUnpaused
        {
            [TestCase(1)]
            [TestCase(23)]
            public void ThenRunnerContinuesToExecuteKanKan(int pauseFor)
            {
                KanKanCallCountSpy kanKanCallCountSpy = new KanKanCallCountSpy();
                IKanKanRunner kanKanRunner = new KanKanSingleRunner(kanKanCallCountSpy, "Scout");
                kanKanRunner.MoveNext();
                Assert.True(kanKanCallCountSpy.MoveNextCallCount == 1);
                kanKanRunner.Pause(true);
                for (int i = 0; i < pauseFor; i++)
                {
                    kanKanRunner.MoveNext();
                }

                kanKanRunner.Pause(false);
                Assert.True(kanKanCallCountSpy.MoveNextCallCount == 1);
                kanKanRunner.MoveNext();
                Assert.True(kanKanCallCountSpy.MoveNextCallCount == 2);
            }
        }
    }
}