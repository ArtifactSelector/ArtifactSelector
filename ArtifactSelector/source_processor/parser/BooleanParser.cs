using ArtifactSelector.artifact_evaluator;
using ArtifactSelector.model;

namespace ArtifactSelector.source_processor
{
    internal class BooleanParser
    {
        public BooleanParser() { }

        public BooleanEvaluator Parse(TokenIterator iterator)
        {
            Token function = iterator.Next();

            switch (function.TokenType)
            {
                case TokenType.FUNCTION_STAT:
                    return ParseStatFunction(iterator);
                case TokenType.FUNCTION_SUB:
                    return ParseSubFunction(iterator);
                case TokenType.FUNCTION_MAIN:
                    return ParseMainFunction(iterator);
                case TokenType.FUNCTION_SLOT:
                    return ParseSlotFunction(iterator);
                case TokenType.FUNCTION_SET:
                    return ParseSetFunction(iterator);
                default:
                    throw new ParsingException(ErrorMessages.ExpectedBooleanFunction, function.LineNum);
            }
        }

        private BooleanEvaluator ParseSubFunction(TokenIterator iterator)
        {
            iterator.Expect(TokenType.PUNC_OPEN_BRACKET);

            Token next = iterator.Next();

            if (next.TokenType == TokenType.STAT_THREE)
            {
                iterator.Expect(TokenType.PUNC_CLOSE_BRACKET);
                return new BooleanEvaluator(artifact => artifact.HasThreeSub());
            }

            if (next.TokenType == TokenType.STAT_FOUR)
            {
                iterator.Expect(TokenType.PUNC_CLOSE_BRACKET);
                return new BooleanEvaluator(artifact => artifact.HasFourSub());
            }

            SubStat subStat = SubStatDictionary.FromToken(next.TokenType);
            if (subStat == SubStat.None)
            {
                throw new ParsingException(ErrorMessages.InvalidSubstatParameter, next.LineNum, next.TokenType);
            }

            iterator.Expect(TokenType.PUNC_CLOSE_BRACKET);

            return new BooleanEvaluator(artifact => artifact.HasSubStat(subStat));
        }

        private BooleanEvaluator ParseMainFunction(TokenIterator iterator)
        {
            iterator.Expect(TokenType.PUNC_OPEN_BRACKET);

            Token next = iterator.Next();
            MainStat mainStat = MainStatDictionary.FromToken(next.TokenType);

            if (mainStat == MainStat.None)
            {
                throw new ParsingException(ErrorMessages.InvalidMainstatParameter, next.LineNum, next.TokenType);
            }

            iterator.Expect(TokenType.PUNC_CLOSE_BRACKET);

            return new BooleanEvaluator(artifact => artifact.Main == mainStat);
        }

        private BooleanEvaluator ParseStatFunction(TokenIterator iterator)
        {
            iterator.Expect(TokenType.PUNC_OPEN_BRACKET);

            Token next = iterator.Next();
            MainStat mainStat = MainStatDictionary.FromToken(next.TokenType);
            SubStat subStat = SubStatDictionary.FromToken(next.TokenType);

            if (mainStat == MainStat.None)
            {
                throw new ParsingException(ErrorMessages.InvalidStatParameter, next.LineNum, next.TokenType);
            }

            iterator.Expect(TokenType.PUNC_CLOSE_BRACKET);

            return new BooleanEvaluator(artifact => artifact.Main == mainStat || artifact.HasSubStat(subStat));
        }

        private BooleanEvaluator ParseSlotFunction(TokenIterator iterator)
        {
            iterator.Expect(TokenType.PUNC_OPEN_BRACKET);

            Token next = iterator.Next();
            GearSlot slot = GearSlotDictionary.FromToken(next.TokenType);

            if (slot == GearSlot.None)
            {
                throw new ParsingException(ErrorMessages.InvalidSubstatParameter, next.LineNum, next.TokenType);
            }

            iterator.Expect(TokenType.PUNC_CLOSE_BRACKET);

            return new BooleanEvaluator(artifact => artifact.Slot == slot);
        }

        private BooleanEvaluator ParseSetFunction(TokenIterator iterator)
        {
            iterator.Expect(TokenType.PUNC_OPEN_BRACKET);

            Token next = iterator.Next();
            ArtifactSet set = ArtifactSetDictionary.FromToken(next.TokenType);

            if (set == ArtifactSet.None)
            {
                throw new ParsingException(ErrorMessages.InvalidSubstatParameter, next.LineNum, next.TokenType);
            }

            iterator.Expect(TokenType.PUNC_CLOSE_BRACKET);

            return new BooleanEvaluator(artifact => artifact.Set == set);
        }
    }
}
