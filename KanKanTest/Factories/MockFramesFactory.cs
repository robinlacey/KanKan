using System;
using System.Reflection;
using System.Reflection.Emit;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Frame.SimpleKarassFrame;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.Dependencies;

namespace KanKanTest.Factories
{
    public class MockFramesFactory
    {
        private int _ticker;
        private readonly IFrameFactory _frameFactory;
        private readonly IDependencies _dependencies;

        public MockFramesFactory(IFrameFactory frameFactory)
        {
            _frameFactory = frameFactory;
            _dependencies = frameFactory.Dependencies;
        }

        Type GetRandomType()
        {
            string name = Guid.NewGuid() + "-" + _ticker;
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(name),
                AssemblyBuilderAccess.Run);
            ModuleBuilder mb = ab.DefineDynamicModule(name);
            return mb.DefineType(
                    name,
                    TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable)
                .CreateType();
        }

        public FrameRequest GetValidFrameRequest(Func<string, bool> func)
        {
            Type randomType = GetRandomType();
            Type simpleKarassFrameClass = typeof(SimpleKarassFrame<>);
            Type constructedClass = simpleKarassFrameClass.MakeGenericType(randomType);
            
            object randomKarassFrameAction = Activator.CreateInstance(constructedClass, new object[]
            {
                func,
                new DependenciesDummy()
            });
            dynamic Resolver() => randomKarassFrameAction;
            object[] args =
            {
                (Func<dynamic>) Resolver,
            };

            _dependencies.GetType()
                .GetMethod("Register")
                .MakeGenericMethod(randomKarassFrameAction.GetType())
                .Invoke(_dependencies, args);

            _frameFactory.GetType()
                .GetMethod("RegisterRoute")
                .MakeGenericMethod(randomType, randomKarassFrameAction.GetType())
                .Invoke(_frameFactory, Array.Empty<object>());
            
            _ticker++;
            return new FrameRequest(Activator.CreateInstance(randomType));
        }

        public FrameRequest GetInvalidFrameRequest()
        {
            _ticker++;
            return new FrameRequest(new Random(_ticker).Next(0, int.MaxValue).ToString());
        }
    }
}