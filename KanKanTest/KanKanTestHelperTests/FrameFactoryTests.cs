using KanKanCore.Interface;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanTestHelperTests.Mocks;
using KanKanTestHelper;
using NUnit.Framework;

namespace KanKanTest.KanKanTestHelperTests
{
    public class FrameFactoryTests
    {
        [Test]
        public void KanKanTestHelperAssignedIFrameFactory()
        {
            IFrameFactory frameFactoryDummy = new FrameFactoryDummy();
            TestHelper kanKanTestHelper =
                new TestHelper(new RunKanKanDummy(),frameFactoryDummy);
            Assert.AreSame(kanKanTestHelper.FrameFactory, frameFactoryDummy);
        }
    }
}