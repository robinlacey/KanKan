using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
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
            TestHelper kanKanTestHelper = new TestHelper(new RunKanKanDummy(), new KanKanDummy());
            Assert.True((IRunKanKan) kanKanTestHelper.Run != null);
        }

        [Test]
        public void KanKanTestHelperAssignedIRunKanKan()
        {
            IRunKanKan runKanKanDummy = new RunKanKanDummy();
            TestHelper kanKanTestHelper = new TestHelper(runKanKanDummy, new KanKanDummy());
            Assert.AreSame(kanKanTestHelper.Run, runKanKanDummy);
        }
        
        [Test]
        public void KanKanSetThroughConstructor()
        {
           
            IKanKan kankan = new KanKanDummy();
            IRunKanKan runKanKanDummy = new RunKanKan(kankan, new RunUntilDummy());
            TestHelper kanKanTestHelper = new TestHelper(new RunKanKanDummy(),kankan);
            Assert.AreSame(kankan,runKanKanDummy.KanKan);
        }
    }
}