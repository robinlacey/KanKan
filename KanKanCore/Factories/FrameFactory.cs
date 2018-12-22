using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Factories
{
    public class FrameFactory : IFrameFactory
    {
        private IDependencies Dependencies { get; }

        private readonly Dictionary<Type, Type> _routing = new Dictionary<Type, Type>();

        public FrameFactory(IDependencies dependencies)
        {
            Dependencies = dependencies;
        }

       
        public void RegisterRoute<TRequestType, TKarassFrameType>() where TKarassFrameType : IKarassFrame<TRequestType>
        {
            _routing.Add(typeof(TRequestType), typeof(TKarassFrameType));
        }

        public IKarassFrame<T> Get<T>()
        {
            // Credit to @craigjbass 
            object dependency = 
                Dependencies.GetType()
                .GetMethod("Get")
                .MakeGenericMethod(_routing[typeof(T)])
                .Invoke(Dependencies, Array.Empty<object>());
            return (IKarassFrame<T>) dependency;
        }

        public bool Execute(FrameRequest frameRequest, string message)
        {
            object karassFrameObject = 
                Dependencies.GetType().GetMethod("Get")
                .MakeGenericMethod(_routing[frameRequest.RequestType])
                .Invoke(Dependencies, Array.Empty<object>());

            object returnValue = karassFrameObject.GetType().GetMethod("Execute")
                .Invoke(karassFrameObject, new[] {message, frameRequest.RequestObject});
            return (bool) returnValue;
        }
    }
}