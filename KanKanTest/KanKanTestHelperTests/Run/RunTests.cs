using KanKanCore.Interface;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanTestHelperTests.Mocks;
using KanKanTestHelper;
using KanKanTestHelper.Interface;
using KanKanTestHelper.Run;
using NUnit.Framework;

namespace KanKanTest.KanKanTestHelperTests.Run
{
    public class RunTests
    {
        [Test]
        public void KanKanTestHelperHasAnIRunKanKan()
        {
            TestHelper kanKanTestHelper = new TestHelper(new RunKanKanDummy(), new KanKanDummy(),new FrameFactoryDummy());
            Assert.True((IRunKanKan) kanKanTestHelper.Run != null);
        }

        [Test]
        public void KanKanTestHelperAssignedIRunKanKan()
        {
            IRunKanKan runKanKanDummy = new RunKanKanDummy();
            TestHelper kanKanTestHelper = new TestHelper(runKanKanDummy, new KanKanDummy(),new FrameFactoryDummy());
            Assert.AreSame(kanKanTestHelper.Run, runKanKanDummy);
        }
        
        [Test]
        public void KanKanSetThroughConstructor()
        {
            IKanKan kankan = new KanKanDummy();
            IRunKanKan runKanKanDummy = new RunKanKan(kankan, new RunUntilDummy());
            Assert.AreSame(kankan,runKanKanDummy.KanKan);
        }
    }
}