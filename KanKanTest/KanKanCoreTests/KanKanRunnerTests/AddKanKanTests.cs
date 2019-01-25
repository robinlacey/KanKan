using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class GivenASingleKanKan
    {
        public class AddKanKanTests
        {
            [TestCase("Scout")]
            [TestCase("Dog")]
            public void ThenCurrentKanKanIsNotChanged(string tag)
            {
                IKanKan kanKanDummy = new KanKanDummy();
                IKanKanSingleRunner kanKanRunner = new KanKanSingleRunner(kanKanDummy, $"Different_{tag}");
                IKanKan addedKanKan = new KanKanDummy();
                kanKanRunner.Add(addedKanKan, tag);
                Assert.AreNotSame(addedKanKan, kanKanRunner.Current);
            }
        }
    }
}