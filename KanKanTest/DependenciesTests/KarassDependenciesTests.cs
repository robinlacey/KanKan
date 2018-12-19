using System;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Interface;
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
            public readonly string _returnValue;

            public TestClass(string returnValue)
            {
                _returnValue = returnValue;
            }
            public string Test()
            {
                return _returnValue;
            }
        }

        [TestCase("Doggo", ExpectedResult= "Doggo")]
        [TestCase("Scout", ExpectedResult= "Scout")]
        public string ThenCorrectlyReturnType(string name)
        {
            KarassDependencies dependencies = new KarassDependencies();
            TestClass testClass = new TestClass(name);
            dependencies.Register<ITestInterface>(() => testClass);
            ITestInterface t = dependencies.Get<ITestInterface>();
            return t.Test();
        }
  
    }
}