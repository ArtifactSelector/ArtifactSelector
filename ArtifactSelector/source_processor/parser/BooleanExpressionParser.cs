using ArtifactSelector.artifact_evaluator;
using System.Collections.Generic;

namespace ArtifactSelector.source_processor
{
    public class BooleanExpressionParser
    {
        private readonly BooleanParser booleanParser = new BooleanParser();

        public BooleanEvaluator Parse(TokenIterator iterator, TokenType endTokenType, Dictionary<string, BooleanEvaluator> identifierMap)
        {
            return Parse(iterator, new HashSet<TokenType> { endTokenType }, identifierMap);
        }

        public BooleanEvaluator Parse(TokenIterator iterator, HashSet<TokenType> endTokenTypes, Dictionary<string, BooleanEvaluator> identifierMap)
        {
            return EvaluatePostfix(ToPostfix(iterator, endTokenTypes), identifierMap);
        }


        public List<Token> ToPostfix(TokenIterator iterator, HashSet<TokenType> endTokenTypes)
        {
            List<Token> postfixExpression = new List<Token>();
            List<Token> operatorStack = new List<Token>();
            int expressionLineNum = iterator.Peek().LineNum;

            while (!endTokenTypes.Contains(iterator.Peek().TokenType))
            {
                Token current = iterator.Next();

                if (current.TokenType == TokenType.IDENTIFIER)
                {
                    postfixExpression.Add(current);
                }
                else if (current.IsFunction())
                {
                    postfixExpression.Add(current);
                    postfixExpression.Add(iterator.Next());
                    postfixExpression.Add(iterator.Next());
                    postfixExpression.Add(iterator.Next());

                }
                else if (current.TokenType == TokenType.PUNC_OPEN_BRACKET)
                {
                    operatorStack.Add(current);
                }
                else if (current.TokenType == TokenType.PUNC_CLOSE_BRACKET)
                {
                    while (operatorStack.Count > 0 && operatorStack[operatorStack.Count - 1].TokenType != TokenType.PUNC_OPEN_BRACKET)
                    {
                        postfixExpression.Add(operatorStack[operatorStack.Count - 1]);
                        operatorStack.RemoveAt(operatorStack.Count - 1);
                    }

                    if (operatorStack.Count == 0)
                    {
                        throw new ParsingException(ErrorMessages.SyntaxUnmatchedBracket, current.LineNum);
                    }

                    operatorStack.RemoveAt(operatorStack.Count - 1); // Remove open bracket
                }
                else if (IsBooleanOperator(current))
                {
                    // Ensure that not operator is on the left
                    if (current.TokenType == TokenType.OPERATOR_NOT)
                    {
                        Token next = iterator.Peek();
                        if (IsBooleanOperator(next))
                        {
                            throw new ParsingException(ErrorMessages.UnexpectedTokenInBooleanExpression, current.LineNum, current.TokenType);
                        }
                    }

                    while (operatorStack.Count > 0 && GetPrecedence(operatorStack[operatorStack.Count - 1]) > GetPrecedence(current))
                    {
                        postfixExpression.Add(operatorStack[operatorStack.Count - 1]);
                        operatorStack.RemoveAt(operatorStack.Count - 1);
                    }
                    operatorStack.Add(current);
                }
                else
                {
                    throw new ParsingException(ErrorMessages.UnexpectedTokenInBooleanExpression, current.LineNum, current.TokenType);
                }

            }

            while (operatorStack.Count > 0)
            {
                postfixExpression.Add(operatorStack[operatorStack.Count - 1]);
                operatorStack.RemoveAt(operatorStack.Count - 1);
            }

            if (postfixExpression.Count == 0)
            {
                throw new ParsingException(ErrorMessages.ExpectedBooleanExpression, expressionLineNum);
            }

            return postfixExpression;
        }

        public BooleanEvaluator EvaluatePostfix(List<Token> postfix, Dictionary<string, BooleanEvaluator> identifierMap)
        {
            List<BooleanEvaluator> stack = new List<BooleanEvaluator>();
            TokenIterator iterator = new TokenIterator(postfix);
            int lineNum = iterator.Peek().LineNum;

            while (iterator.HasNext())
            {
                Token token = iterator.Peek();

                if (token.TokenType == TokenType.IDENTIFIER)
                {
                    iterator.Next();
                    if (identifierMap.ContainsKey(token.TokenValue))
                    {
                        stack.Add(identifierMap[token.TokenValue]);
                    }
                    else
                    {
                        throw new ParsingException(ErrorMessages.ReferencedBeforeDeclared, token.LineNum, token.TokenValue);
                    }
                }
                else if (token.IsFunction())
                {
                    BooleanEvaluator eval = booleanParser.Parse(iterator);
                    stack.Add(eval);
                }
                else if (token.TokenType == TokenType.OPERATOR_NOT)
                {
                    iterator.Next();
                    if (stack.Count < 1)
                    {
                        throw new ParsingException(ErrorMessages.SyntaxErrorInBooleanExpression, token.LineNum);
                    }
                    stack[stack.Count - 1] = stack[stack.Count - 1].Negate();
                }
                else if (token.TokenType == TokenType.OPERATOR_AND || token.TokenType == TokenType.OPERATOR_OR)
                {
                    iterator.Next();
                    if (stack.Count < 2)
                    {
                        throw new ParsingException(ErrorMessages.SyntaxErrorInBooleanExpression, token.LineNum);
                    }

                    BooleanEvaluator second = stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);
                    BooleanEvaluator first = stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);

                    if (token.TokenType == TokenType.OPERATOR_AND)
                    {
                        stack.Add(first.And(second));
                    }
                    else
                    {
                        stack.Add(first.Or(second));
                    }
                }
                else
                {
                    throw new ParsingException(ErrorMessages.SyntaxErrorInBooleanExpression, token.LineNum);
                }
            }

            if (stack.Count == 1)
            {
                return stack[0];
            }
            else
            {
                throw new ParsingException(ErrorMessages.SyntaxErrorInBooleanExpression, lineNum);
            }

        }

        private bool IsBooleanOperator(Token token)
        {
            return GetPrecedence(token) > 0;
        }

        private int GetPrecedence(Token token)
        {
            TokenType type = token.TokenType;

            if (type == TokenType.OPERATOR_NOT)
            {
                return 3;
            }
            if (type == TokenType.OPERATOR_AND)
            {
                return 2;
            }
            if (type == TokenType.OPERATOR_OR)
            {
                return 1;
            }
            return 0;
        }
    }
}
