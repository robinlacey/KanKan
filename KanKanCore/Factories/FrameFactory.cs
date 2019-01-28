using System;
using System.Collections.Generic;
using System.Reflection;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanCore.Factories
{
    public class FrameFactory : IFrameFactory
    {
        public IDependencies Dependencies { get; }

        private readonly Dictionary<Type, Type> _routing = new Dictionary<Type, Type>();

        public FrameFactory(IDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        public void RegisterRoute<TRequestType, TKarassFrameType>() where TKarassFrameType : IKarassFrame<TRequestType>
        {
            _routing.TryGetValue(typeof(TRequestType), out Type routeType);
            if (routeType!=null)
            {
                _routing[typeof(TRequestType)] = typeof(TKarassFrameType);
            }
            else
            {
                _routing.Add(typeof(TRequestType), typeof(TKarassFrameType));
            }
        }

        public IKarassFrame<T> Get<T>()
        {
            _routing.TryGetValue(typeof(T), out Type routeType);
            if (routeType == null)
            {
                throw new MissingRouteException();
            }

            return GetKarassFrame<T>(routeType);
        }

        public bool Execute(FrameRequest frameRequest, string message)
        {
            _routing.TryGetValue(frameRequest.RequestType, out Type routeType);
            if (routeType == null)
            {
                throw new MissingRouteException();
            }
            
            return ExecuteFrameRequest(frameRequest, message, GetKarassFrameObject(routeType));
        }

        private bool ExecuteFrameRequest(FrameRequest frameRequest, string message, object karassFrameObject)
        {
            try
            {
                return (bool) karassFrameObject
                    .GetType()
                    .GetMethod("Execute")
                    .Invoke(karassFrameObject, new[]
                    {
                        message,
                        frameRequest.RequestObject
                    });
            }
            catch (TargetInvocationException)
            {
                throw new MissingDependencyException(frameRequest.RequestType);
            }
        }

        private IKarassFrame<T> GetKarassFrame<T>(Type routeType)
        {
            try
            {
                return (IKarassFrame<T>)
                    Dependencies.GetType()
                        .GetMethod("Get")
                        .MakeGenericMethod(routeType)
                        .Invoke(Dependencies, Array.Empty<object>());
            }
            catch (TargetInvocationException)
            {
                throw new MissingDependencyException(routeType);
            }
        }

        private object GetKarassFrameObject(Type routeType)
        {
            try
            {
                return Dependencies.GetType()
                    .GetMethod("Get")
                    .MakeGenericMethod(routeType)
                    .Invoke(Dependencies, Array.Empty<object>());
            }
            catch (TargetInvocationException)
            {
                throw new MissingDependencyException(routeType);
            }
        }
    }
}