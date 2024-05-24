using ArtifactSelector.artifact_evaluator;
using System.Collections.Generic;

namespace ArtifactSelector.source_processor
{
    internal class IfStatementParser
    {
        private readonly BooleanExpressionParser booleanExpressionParser = new BooleanExpressionParser();
        private readonly ActionParser actionParser = new ActionParser();
        private readonly DeclarationParser declarationParser = new DeclarationParser();

        public ArtifactEvaluator Parse(TokenIterator iterator, Dictionary<string, BooleanEvaluator> identifierMap)
        {
            Token ident = iterator.Next();
            if (ident.TokenType != TokenType.KEYWORD_IF)
            {
                throw new ParsingException(ErrorMessages.UnexpectedToken, ident.LineNum, TokenType.KEYWORD_IF, ident.TokenType);
            }

            if (iterator.Peek().TokenType != TokenType.PUNC_OPEN_BRACKET)
            {
                throw new ParsingException(ErrorMessages.UnexpectedToken, ident.LineNum, TokenType.PUNC_OPEN_BRACKET, iterator.Peek().TokenType);
            }

            BooleanEvaluator boolEval = booleanExpressionParser.Parse(
                iterator, new HashSet<TokenType> { TokenType.OPERATOR_ARROW, TokenType.PUNC_OPEN_CURLY_BRACKET }, identifierMap);

            Token next = iterator.Peek();
            if (next.TokenType == TokenType.OPERATOR_ARROW)
            {
                iterator.Expect(TokenType.OPERATOR_ARROW);
                ArtifactAction action = actionParser.Parse(iterator);
                iterator.Expect(TokenType.PUNC_SEMICOLON);
                return new ArtifactEvaluator(action, boolEval);
            }
            else if (next.TokenType == TokenType.PUNC_OPEN_CURLY_BRACKET)
            {
                iterator.Expect(TokenType.PUNC_OPEN_CURLY_BRACKET);
                ArtifactEvaluator eval = ParseIfBlock(iterator, identifierMap);
                iterator.Expect(TokenType.PUNC_CLOSE_CURLY_BRACKET);
                return new ArtifactEvaluator(boolEval, eval);
            }

            throw new ParsingException(ErrorMessages.UnexpectedToken, next.LineNum, "Arrow operator or If-block", next.TokenType);
        }

        private ArtifactEvaluator ParseIfBlock(TokenIterator iterator, Dictionary<string, BooleanEvaluator> identifierMap)
        {
            List<ArtifactEvaluator> evalList = new List<ArtifactEvaluator>();
            Dictionary<string, BooleanEvaluator> mapCopy = new Dictionary<string, BooleanEvaluator>(identifierMap);

            while (iterator.HasNext() && iterator.Peek().TokenType != TokenType.PUNC_CLOSE_CURLY_BRACKET)
            {
                Token token = iterator.Peek();

                if (token.TokenType == TokenType.IDENTIFIER)
                {
                    declarationParser.Parse(iterator, mapCopy);
                }
                else if (token.TokenType == TokenType.KEYWORD_IF)
                {
                    evalList.Add(Parse(iterator, mapCopy));
                }
                else if (token.TokenType == TokenType.KEYWORD_RETURN)
                {
                    iterator.Expect(TokenType.KEYWORD_RETURN);
                    ArtifactAction finalAction = actionParser.Parse(iterator);
                    iterator.Expect(TokenType.PUNC_SEMICOLON);
                    return new ArtifactEvaluator(finalAction, evalList);
                }
                else
                {
                    throw new ParsingException(ErrorMessages.UnexpectedToken,
                        iterator.Peek().LineNum, "Declaration, If, or Return statements", iterator.Peek().TokenType);
                }
            }

            return new ArtifactEvaluator(ArtifactAction.None, evalList);
        }
    }
}
