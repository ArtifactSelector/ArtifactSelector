using System.Collections.Generic;

namespace ArtifactSelector.source_processor
{
    public class TokenIterator
    {
        private List<Token> tokens;
        int current = 0;

        public TokenIterator(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public bool HasNext()
        {
            return current < tokens.Count;
        }

        public void AssertHasToken()
        {
            if (!HasNext())
            {
                if (tokens.Count == 0)
                {
                    throw new ParsingException(ErrorMessages.ExpectedMoreTokens, 1);
                }

                throw new ParsingException(ErrorMessages.ExpectedMoreTokens, tokens[tokens.Count - 1].LineNum);
            }
        }

        public Token Next()
        {
            AssertHasToken();
            return tokens[current++];
        }

        public Token Peek()
        {
            AssertHasToken();
            return tokens[current];
        }

        public void Expect(TokenType type)
        {
            if (!HasNext())
            {
                throw new ParsingException(ErrorMessages.UnexpectedToken, tokens[tokens.Count - 1].LineNum, type, "none");
            }

            Token next = Next();
            if (next.TokenType != type)
            {
                throw new ParsingException(ErrorMessages.UnexpectedToken, next.LineNum, type, next.TokenValue);
            }
        }

    }
}
