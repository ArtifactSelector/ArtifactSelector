using ArtifactSelector.artifact_evaluator;
using System.Collections.Generic;

namespace ArtifactSelector.source_processor
{
    internal class Parser
    {
        private readonly DeclarationParser declarationParser = new DeclarationParser();
        private readonly IfStatementParser ifStatementParser = new IfStatementParser();
        private readonly ActionParser actionParser = new ActionParser();

        public ArtifactEvaluator Parse(TokenIterator iterator)
        {
            List<ArtifactEvaluator> evalList = new List<ArtifactEvaluator>();
            Dictionary<string, BooleanEvaluator> identifierMap = new Dictionary<string, BooleanEvaluator>();

            while (iterator.HasNext() && iterator.Peek().TokenType != TokenType.KEYWORD_RETURN)
            {
                Token token = iterator.Peek();

                if (token.TokenType == TokenType.IDENTIFIER)
                {
                    declarationParser.Parse(iterator, identifierMap);
                }
                else if (token.TokenType == TokenType.KEYWORD_IF)
                {
                    evalList.Add(ifStatementParser.Parse(iterator, identifierMap));
                }
                else
                {
                    throw new ParsingException(ErrorMessages.UnexpectedToken, iterator.Peek().LineNum, "Declaration, If, or Return statements", iterator.Peek().TokenType);
                }
            }

            iterator.Expect(TokenType.KEYWORD_RETURN);

            ArtifactAction finalAction = actionParser.Parse(iterator);

            iterator.Expect(TokenType.PUNC_SEMICOLON);

            return new ArtifactEvaluator(finalAction, evalList);
        }
    }
}
