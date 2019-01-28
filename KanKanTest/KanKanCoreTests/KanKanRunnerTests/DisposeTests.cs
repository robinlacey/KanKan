using KanKanCore.Interface;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKanRunner;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class DisposeTests
    {
        [Test]
        public void DisposeClearsKanKanList()
        {
           KanKanRunnerSpy<IKanKan> kankanRunnerSpy = new KanKanRunnerSpy<IKanKan>(new KanKanDummy(),"doggo");
           kankanRunnerSpy.Add(new KanKanDummy(), "Cats");
           Assert.True(kankanRunnerSpy.TotalKanKan == 2);
           kankanRunnerSpy.Dispose();
           Assert.True(kankanRunnerSpy.TotalKanKan == 0);
        }
    }
}