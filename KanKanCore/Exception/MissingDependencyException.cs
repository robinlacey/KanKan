using System;

namespace KanKanCore.Exception
{
    public class MissingDependencyException : System.Exception
    {
        private readonly Type _type;
        public MissingDependencyException(Type type)
        {
            _type = type;
        }
        public override string Message => $"No dependency found. Register dependency of type {_type} in IDependency.";
    }
}