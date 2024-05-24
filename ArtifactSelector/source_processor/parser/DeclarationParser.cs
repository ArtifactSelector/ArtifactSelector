using ArtifactSelector.artifact_evaluator;
using System.Collections.Generic;

namespace ArtifactSelector.source_processor
{
    internal class DeclarationParser
    {
        private readonly BooleanExpressionParser booleanExpressionParser = new BooleanExpressionParser();

        public void Parse(TokenIterator iterator, Dictionary<string, BooleanEvaluator> identifierMap)
        {
            Token ident = iterator.Next();
            if (ident.TokenType != TokenType.IDENTIFIER)
            {
                throw new ParsingException(ErrorMessages.UnexpectedToken, ident.LineNum, TokenType.IDENTIFIER, ident.TokenType);
            }

            iterator.Expect(TokenType.OPERATOR_EQUAL);

            identifierMap[ident.TokenValue] = booleanExpressionParser.Parse(iterator, TokenType.PUNC_SEMICOLON, identifierMap);

            iterator.Expect(TokenType.PUNC_SEMICOLON);
        }
    }
}
