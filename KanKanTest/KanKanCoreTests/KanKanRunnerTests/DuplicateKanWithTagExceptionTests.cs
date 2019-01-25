using KanKanCore.Exception;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class DuplicateKanWithTagExceptionTests
    {
        [TestCase("Dog")]
        [TestCase("Cat")]
        [TestCase("Pig")]
        public void ThenMessageContainsTagName(string tag)
        {
            Assert.True(new DuplicateKanKanTag(tag).Message.Contains(tag));
        }
    }
}