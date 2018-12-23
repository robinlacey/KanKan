using KanKanCore.Exception;
using KanKanCore.Karass.Dependencies;
using NUnit.Framework;

namespace KanKanTest.DependenciesTests
{
    public class GivenValidInput
    {
        private interface ITestInterface
        {
            string Test();
        }

        private class TestClass : ITestInterface
        {
            private readonly string _returnValue;

            public TestClass(string returnValue)
            {
                _returnValue = returnValue;
            }

            public string Test()
            {
                return _returnValue;
            }
        }

        [TestCase("Doggo", ExpectedResult = "Doggo")]
        [TestCase("Scout", ExpectedResult = "Scout")]
        public string ThenCorrectlyReturnType(string name)
        {
            KarassDependencies dependencies = new KarassDependencies();
            TestClass testClass = new TestClass(name);
            dependencies.Register<ITestInterface>(() => testClass);
            ITestInterface t = dependencies.Get<ITestInterface>();
            return t.Test();
        }
    }

    public class GivenInvalidInput
    {
        [Test]
        public void ThenThrowMissingDependencyException()
        {
            Assert.Throws<MissingDependencyException>(() => new KarassDependencies().Get<string>());
        }

        public class WhenInterfaceIsAlreadyRegistered
        {
            
            private interface ITestInterface  {}

            private class TestClass : ITestInterface {}
            
            [Test]
            public void ThenDoNotThrowException()
            {
                KarassDependencies dependencies = new KarassDependencies();
                TestClass testClass = new TestClass();
                dependencies.Register<ITestInterface>(() => testClass);
                Assert.DoesNotThrow(()=>dependencies.Register<ITestInterface>(() => testClass));
            }
        }
    }
}