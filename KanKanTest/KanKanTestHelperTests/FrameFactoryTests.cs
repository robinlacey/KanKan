using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
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
                new TestHelper(new RunKanKanDummy(), new KanKan(new KarassDummy(), frameFactoryDummy),frameFactoryDummy);
            Assert.AreSame(kanKanTestHelper.FrameFactory, frameFactoryDummy);
        }
    }
}