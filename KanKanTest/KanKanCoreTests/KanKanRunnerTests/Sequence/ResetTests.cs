using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests.Sequence
{
    public class ResetTests
    {
        [Test]
        public void ConstructorKanKanIsResetToCurrent()
        {
            KanKanCallCountSpy kanKanCallCountSpyOne = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpyTwo = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpyThree = new KanKanCallCountSpy();

            IKanKan[] kanKansOne = {kanKanCallCountSpyOne, kanKanCallCountSpyTwo, kanKanCallCountSpyThree};
            IKanKan[] kanKansTwo = {new KanKanDummy(), new KanKanDummy(), new KanKanDummy()};
            IKanKan[] kanKansThree = {new KanKanDummy(), new KanKanDummy(), new KanKanDummy()};

            IKanKanSequenceRunner kanKanRunner = new KanKanSequenceRunner(kanKansOne, "Cats");
            kanKanRunner.Add(kanKansTwo, "Dogs");
            kanKanRunner.Add(kanKansThree, "Cows");

            Assert.AreSame(kanKanRunner.Current, kanKanCallCountSpyOne);

            kanKanRunner.Run("Cows");

            Assert.AreSame(kanKanRunner.Current, kanKanCallCountSpyThree);

            kanKanRunner.Reset();
            Assert.AreSame(kanKanRunner.Current, kanKanCallCountSpyOne);
        }
        [Test]
        public void ResetRunsResetOnAllKanKans()
        {
            Assert.Fail();
        }

    }
}