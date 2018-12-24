using KanKanCore.Karass;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KarassTests
{
    public class KarassTests
    {
        [Test]
        public void KarassHasUniqueID()
        {
            Assert.False(string.IsNullOrEmpty(new Karass(null,null,null).ID));
            Assert.False(string.IsNullOrWhiteSpace(new Karass(null,null,null).ID));
            Assert.False(new Karass(null,null,null).ID ==new Karass(null,null,null).ID );
        }
        
        [Test]
        public void KarassStateHasUniqueID()
        {
            Assert.False(string.IsNullOrEmpty(new KarassState(new Karass(null,null,null)).ID));
            Assert.False(string.IsNullOrWhiteSpace((new KarassState(new Karass(null,null,null)).ID)));
            Assert.False(new KarassState(new Karass(null,null,null)).ID == new KarassState(new Karass(null,null,null)).ID );
        }
    }
}