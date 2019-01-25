using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class ResetTests
    {
        [Test]
        public void ResetRunsResetOnAllKanKans()
        {
            KanKanCallCountSpy kanKanCallCountSpyOne = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpyTwo = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpyThree = new KanKanCallCountSpy();

            IKanKanSingleRunner kanKanRunner = new KanKanSingleRunner(kanKanCallCountSpyOne, "Cats");
            kanKanRunner.Add(kanKanCallCountSpyTwo, "Dogs");
            kanKanRunner.Add(kanKanCallCountSpyThree, "Cows");
            kanKanRunner.Reset();

            Assert.True(kanKanCallCountSpyOne.ResetCallCount == 1);
            Assert.True(kanKanCallCountSpyTwo.ResetCallCount == 1);
            Assert.True(kanKanCallCountSpyThree.ResetCallCount == 1);
        }

        [Test]
        public void ConstructorKanKanIsResetToCurrent()
        {
            KanKanCallCountSpy kanKanCallCountSpyOne = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpyTwo = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpyThree = new KanKanCallCountSpy();

            IKanKanSingleRunner kanKanRunner = new KanKanSingleRunner(kanKanCallCountSpyOne, "Cats");
            kanKanRunner.Add(kanKanCallCountSpyTwo, "Dogs");
            kanKanRunner.Add(kanKanCallCountSpyThree, "Cows");

            Assert.AreSame(kanKanRunner.Current, kanKanCallCountSpyOne);

            kanKanRunner.Run("Cows");

            Assert.AreSame(kanKanRunner.Current, kanKanCallCountSpyThree);

            kanKanRunner.Reset();
            Assert.AreSame(kanKanRunner.Current, kanKanCallCountSpyOne);
        }

        [TestCase(3)]
        [TestCase(9)]
        public void PauseIsDisabled(int pauseFor)
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

            kanKanRunner.Reset();
            Assert.True(kanKanCallCountSpy.MoveNextCallCount == 1);
            kanKanRunner.MoveNext();
            Assert.True(kanKanCallCountSpy.MoveNextCallCount == 2);
        }
    }
}