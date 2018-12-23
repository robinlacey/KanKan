namespace KanKanCore.Exception
{
    public class MissingDependencyException : System.Exception
    {
        public override string Message => "No dependency found. Register dependency in IDependency.";
    }
}