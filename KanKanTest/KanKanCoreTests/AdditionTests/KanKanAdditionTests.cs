using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.AdditionTests
{
    public class KanKanAdditionTests
    {
        public class CreatesAUniqueKanKan
        {
            [Test]
            public void IDDoesNotMatch()
            {
              KanKan kanKanOne = new KanKan(new KarassDummy(), null);
              KanKan kanKanTwo = new KanKan(new KarassDummy(), null);

             // KanKan kanKanThree = kanKanOne + kanKanTwo;
            }
        }
    }
}