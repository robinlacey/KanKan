using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Interface;
using Xunit;

namespace KanKanTest.DependenciesTests
{
    public class KarassDependenciesTests
    {
        public class GivenValidInput
        {
            public class ExampleOne
            {
                private interface ITestInterface
                {
                    string Test();
                }

                private class TestClass : ITestInterface
                {
                    public string Test()
                    {
                        return "Doggo";
                    }
                }

                [Fact]
                public void ThenCorrectlyReturnType()
                {
                    KarassDependencies dependencies = new KarassDependencies();
                    dependencies.Register<ITestInterface>(new TestClass());
                    Assert.True(dependencies.Get<ITestInterface>().Test() == "Doggo");

                }
            }
            
            public class ExampleTwo
            {
                
                private interface ITestScoutInterface
                {
                    string Test();
                }

                private class TestScoutClass : ITestScoutInterface
                {
                
                    public string Test()
                    {
                        return "Scout";
                    }
                }

                [Fact]
                public void ThenCorrectlyReturnType()
                {
                    KarassDependencies dependencies = new KarassDependencies();
                    dependencies.Register<ITestScoutInterface>(new TestScoutClass());
                    Assert.True(dependencies.Get<ITestScoutInterface>().Test() == "Scout");

                }
            }
            
        }
    }
}