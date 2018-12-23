namespace KanKanCore.Exception
{
    public class MissingRouteException : System.Exception
    {
        public override string Message => "No route was found. Register route in IFrameFactory";
    }
}