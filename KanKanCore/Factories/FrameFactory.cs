using System;
using System.Collections.Generic;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Factories
{
    public class FrameFactory
    {
        private readonly IDependencies _dependencies;
        private readonly Dictionary<Type,Type> _routing = new Dictionary<Type, Type>();
        public FrameFactory(IDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public void RegisterRoute<TRequestType, TKarassFrameType>() where TKarassFrameType : IKarassFrame<TRequestType>
        {
            _routing.Add(typeof(TRequestType), typeof(TKarassFrameType));
        }

        public IKarassFrame<T> Get<T>()
        {
            // Credit to @craigjbass
            object dependency = _dependencies.GetType().GetMethod("Get").MakeGenericMethod(_routing[typeof(T)])
                .Invoke(_dependencies, Array.Empty<object>());
            return (IKarassFrame<T>) dependency;
        }
    }
}