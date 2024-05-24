using ArtifactSelector.artifact_evaluator;

namespace ArtifactSelector.source_processor
{
    internal class ActionParser
    {
        public ArtifactAction Parse(TokenIterator iterator)
        {
            Token action = iterator.Next();

            switch (action.TokenType)
            {
                case TokenType.ACTION_LOCK:
                    return ArtifactAction.Lock;
                case TokenType.ACTION_KEEP:
                    return ArtifactAction.Keep;
                case TokenType.ACTION_TRASH:
                    return ArtifactAction.Trash;
                default:
                    throw new ParsingException(ErrorMessages.UnexpectedToken, action.LineNum, "action keywords", action.TokenType);
            }
        }
    }
}
