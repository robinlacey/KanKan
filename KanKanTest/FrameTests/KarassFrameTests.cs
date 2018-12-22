using KanKanCore.Karass.Frame.SimpleKarassFrame;
using NUnit.Framework;

namespace KanKanTest.FrameTests
{
    public class KarassFrameTests
    {
        public class SimpleKarassFrameTests
        {
            [TestCase("Scout")]
            [TestCase("Is")]
            [TestCase("A")]
            [TestCase("Good")]
            [TestCase("Dog")]
            [TestCase(null)]
            public void SimpleKarassFrameExecutesFunc(string testMessage)
            {
                bool simpleFrameSpyRun = false;
                string simpleFrameSpyMessage = string.Empty;

                bool SimpleFrameSpy(string message)
                {
                    simpleFrameSpyRun = true;
                    simpleFrameSpyMessage = message;
                    return true;
                }

                SimpleKarassFrame<object> simpleKarassFrame = new SimpleKarassFrame<object>(SimpleFrameSpy, null);
                simpleKarassFrame.Execute(testMessage, new object());
                Assert.True(simpleFrameSpyRun);
                Assert.True(simpleFrameSpyMessage == testMessage);
            }
        }
    }
}