using KanKanCore.Factories;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.KarassFrame.FrameStruct;
using NUnit.Framework;

namespace KanKanTest.FrameTests.Degenerates
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