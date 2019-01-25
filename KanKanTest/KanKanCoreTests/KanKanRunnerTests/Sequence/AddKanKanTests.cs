using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests.Sequence
{
    public class AddKanKanTests
    {
        [TestCase("Scout")]
        [TestCase("Dog")]
        public void ThenCurrentKanKanIsNotChanged(string tag)
        {
            IKanKan[] kanKanDummyOne = {new KanKanDummy(), new KanKanDummy(), new KanKanDummy()};
            IKanKan[] kanKanDummyTwo = {new KanKanDummy(), new KanKanDummy(), new KanKanDummy()};
            KanKanSequenceRunner kanKanRunner = new KanKanSequenceRunner(kanKanDummyOne, $"Different_{tag}");
            IKanKan addedKanKan = new KanKanDummy();
            kanKanRunner.Add(kanKanDummyTwo, tag);
            Assert.AreNotSame(addedKanKan, kanKanRunner.Current);
        }
    }
}