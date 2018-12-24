using KanKanCore.Factories;
using KanKanCore.Karass.Interface;
using KanKanTest.KanKanCoreTests.Mocks.Dependencies;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame.FrameStruct;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.FrameTests.Degenerates
{
    public class GivenDuplicateRegistrationInFrameFactory
    {
        [Test]
        public void ThenDoesNotThrowArgumentException()
        {
            IDependencies dependencies = new DependenciesDummy();
            FrameFactory frameFactory = new FrameFactory(dependencies);
            frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();
            Assert.DoesNotThrow(
                () => frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>());
        }
    }
}