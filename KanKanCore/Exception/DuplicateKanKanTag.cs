namespace KanKanCore.Exception
{
    public class DuplicateKanKanTag : System.Exception
    {
        public override string Message { get; }

        public DuplicateKanKanTag(string tag)
        {
            Message = $"Duplicate KanKan with tag {tag}";
        }
    }
}