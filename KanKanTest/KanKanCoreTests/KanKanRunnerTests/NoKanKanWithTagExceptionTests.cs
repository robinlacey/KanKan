using KanKanCore.Exception;
using KanKanCore.Karass.Frame;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class NoKanKanWithTagExceptionTests
    {
        [TestCase("Dog")]
        [TestCase("Cat")]
        [TestCase("Pig")]
        public void ThenMessageContainsTagName(string tag)
        {
            Assert.True(new NoKanKanWithTag(tag).Message.Contains(tag));
        }
    }
   
    public class MissingDependencyExceptionTests
    {
        [TestCase("Dog")]
        [TestCase("Cat")]
        [TestCase("Pig")]
        public void ThenMessageContainsFrameRequestInformation(string frameRequestString)
        {
            Assert.True(new MissingDependencyException(frameRequestString.GetType()).Message.Contains(frameRequestString.GetType().ToString()));
        }
    }
}