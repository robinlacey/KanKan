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

            IKanKan[] kanKansOne = {kanKanCallCountSpyOne, new KanKanDummy(), };
            IKanKan[] kanKansTwo = {kanKanCallCountSpyTwo, new KanKanDummy(), new KanKanDummy()};
            IKanKan[] kanKansThree = {kanKanCallCountSpyThree, new KanKanDummy(), new KanKanDummy()};

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
            KanKanCallCountSpy kanKanCallCountSpyOne = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpyTwo = new KanKanCallCountSpy();

            IKanKan[] kanKansOne = {kanKanCallCountSpyOne, kanKanCallCountSpyTwo};

            KanKanCallCountSpy kanKanCallCountSpyThree = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpyFour = new KanKanCallCountSpy();

            IKanKan[] kanKansTwo = {kanKanCallCountSpyThree, kanKanCallCountSpyFour};

            KanKanCallCountSpy kanKanCallCountSpyFive = new KanKanCallCountSpy();
            KanKanCallCountSpy kanKanCallCountSpySix = new KanKanCallCountSpy();

            IKanKan[] kanKansThree = {kanKanCallCountSpyFive, kanKanCallCountSpySix};

            IKanKanSequenceRunner kanKanRunner = new KanKanSequenceRunner(kanKansOne, "Cats");
            kanKanRunner.Add(kanKansTwo, "Dogs");
            kanKanRunner.Add(kanKansThree, "Cows");

            kanKanRunner.Reset();
            
            Assert.True(kanKanCallCountSpyOne.ResetCallCount == 1);
            Assert.True(kanKanCallCountSpyTwo.ResetCallCount == 1);
            Assert.True(kanKanCallCountSpyThree.ResetCallCount == 1);
            Assert.True(kanKanCallCountSpyFour.ResetCallCount == 1);
            Assert.True(kanKanCallCountSpyFive.ResetCallCount == 1);
            Assert.True(kanKanCallCountSpySix.ResetCallCount == 1);
        }
    }
}