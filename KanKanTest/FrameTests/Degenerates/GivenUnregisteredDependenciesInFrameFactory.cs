using System;
using KanKanCore.Exception;
using KanKanCore.Factories;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.KarassFrame;
using KanKanTest.Mocks.KarassFrame.FrameStruct;
using NUnit.Framework;

namespace KanKanTest.FrameTests.Degenerates
{
    public class GivenUnregisteredDependenciesInFrameFactory
    {
        public class WhenRouteHasNotBeenRegisteredDuringExecute
        {
            readonly IDependencies _dependencies = new KarassDependencies();
            private FrameFactory _frameFactory;

            [SetUp]
            public void Setup()
            {
                _frameFactory = new FrameFactory(_dependencies);
            }

            [Test]
            public void ThenThrowsMissingRouteException()
            {
                KarassFrameDummy<FrameStructDummy> dummy = new KarassFrameDummy<FrameStructDummy>(_dependencies);
                FrameRequest frameRequest = new FrameRequest(new FrameStructDummy());
                _dependencies.Register<IKarassFrame<FrameStructDummy>>(() => dummy);
                Assert.Throws<MissingRouteException>(() => _frameFactory.Execute(frameRequest, String.Empty));
            }
        }

        public class WhenNoDependencyDuringExecute
        {
            readonly IDependencies _dependencies = new KarassDependencies();
            private FrameFactory _frameFactory;

            [SetUp]
            public void Setup()
            {
                _frameFactory = new FrameFactory(_dependencies);
            }

            [Test]
            public void ThenThrowsMissingRouteException()
            {
                KarassFrameDummy<FrameStructDummy> dummy = new KarassFrameDummy<FrameStructDummy>(_dependencies);
                FrameRequest frameRequest = new FrameRequest(new FrameStructDummy());
                _frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();
                Assert.Throws<MissingDependencyException>(() => _frameFactory.Execute(frameRequest, String.Empty));
            }
        }

        public class WhenRouteHasNotBeenRegisteredDuringGet
        {
            readonly IDependencies _dependencies = new KarassDependencies();
            private FrameFactory _frameFactory;

            [SetUp]
            public void Setup()
            {
                _frameFactory = new FrameFactory(_dependencies);
            }

            [Test]
            public void ThenThrowsMissingRouteException()
            {
                KarassFrameDummy<FrameStructDummy> dummy = new KarassFrameDummy<FrameStructDummy>(_dependencies);
                _dependencies.Register<IKarassFrame<FrameStructDummy>>(() => dummy);
                Assert.Throws<MissingRouteException>(() => _frameFactory.Get<FrameStructDummy>());
            }
        }

        public class WhenDependencyHasNotBeenRegisteredDuringGet
        {
            readonly IDependencies _dependencies = new KarassDependencies();
            private FrameFactory _frameFactory;

            [SetUp]
            public void Setup()
            {
                _frameFactory = new FrameFactory(_dependencies);
            }

            [Test]
            public void ThenThrowsMissingRouteException()
            {
                KarassFrameDummy<FrameStructDummy> dummy = new KarassFrameDummy<FrameStructDummy>(_dependencies);
                _frameFactory.RegisterRoute<FrameStructDummy, KarassFrameDummy<FrameStructDummy>>();
                Assert.Throws<MissingDependencyException>(() => _frameFactory.Get<FrameStructDummy>());
            }
        }
    }
}