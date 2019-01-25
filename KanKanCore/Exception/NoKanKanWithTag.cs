namespace KanKanCore.Exception
{
    public class NoKanKanWithTag: System.Exception
    {
        public NoKanKanWithTag(string tag)
        {
            Message = $"Could not find KanKan with tag {tag}";
        }
        public override string Message { get; }
    }
}