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

              KanKan kanKanThree = kanKanOne + kanKanTwo;
              Assert.False(kanKanThree.ID ==  kanKanOne.ID);
              Assert.False(kanKanThree.ID ==  kanKanTwo.ID);
             
            }

            public class GivenSingleKarassInBothKanKan
            {
                
                [Test]
                public void KarassAreCombined()
                {
                    Assert.True(1==2);
                    // K1[0] +K2[0]
                }
            }

            public class GivenMultipleKarassInKanKan
            {
                [Test]
                public void MultipleKarassAreCombined()
                {
                    Assert.True(1==2);
                    // K1[0] +K2[0]
                    // K1[1]
                    // K1[2]
                    
                    // K1[0] +K2[0]
                    // K1[1] +K2[1]
                    // K2[2]
                }
            }
        }
    }
}