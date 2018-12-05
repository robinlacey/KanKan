using KanKanCore;
using KanKanCore.Karass;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest.IEnumeratorTests
{
    public class KanKanReturnTests
    {
        public class GivenNoFrames
        {
            [Fact]
            void GivenNoFramesReturnFalse()
            {
                Karass karass = new KarassDummy();
                KanKan kankan = new KanKan(karass, new KarassMessageDummy());
                Assert.False(kankan.MoveNext());
            }
        }

        public class GivenMultipleFrames
        {
            [Fact]
            void WithTwoFramesReturnCorrectValues()
            {
                IKarass karass = new KarassNumberOfFramesStub(2);
                KanKan kankan = new KanKan(karass, new KarassMessageDummy());
                Assert.True(kankan.MoveNext());
                Assert.False(kankan.MoveNext());
            }

            [Fact]
            void WithThreeFramesReturnCorrectValues()
            {
                IKarass karass = new KarassNumberOfFramesStub(2);
                KanKan kankan = new KanKan(karass, new KarassMessageDummy());
                Assert.True(kankan.MoveNext());
                Assert.False(kankan.MoveNext());
            }

            [Fact]
            void WithMultipleFramesReturnCorrectValues()
            {
                int number = 42;
                IKarass karass = new KarassNumberOfFramesStub(number);
                KanKan kankan = new KanKan(karass, new KarassMessageDummy());
                for (int i = 0; i < number - 1; i++)
                {
                    bool returnValue = kankan.MoveNext();
                    Assert.True(returnValue);
                }

                Assert.False(kankan.MoveNext());
            }
        }
    }
}