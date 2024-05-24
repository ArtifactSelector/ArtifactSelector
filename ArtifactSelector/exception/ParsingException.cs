namespace ArtifactSelector
{
    public class ParsingException : SelectorException
    {
        public ParsingException(string message, int lineNum, params object[] args) : base(string.Format(message, args) + "(Line " + lineNum + ")")
        {
        }
    }

}
