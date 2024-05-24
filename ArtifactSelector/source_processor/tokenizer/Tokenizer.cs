using System.Collections.Generic;

namespace ArtifactSelector.source_processor
{

    internal class Tokenizer
    {
        private readonly Dictionary<string, TokenType> tokenMap = new Dictionary<string, TokenType>
        {
            { Constants.EQUAL.ToString(), TokenType.OPERATOR_EQUAL },
            { Constants.AND_OPERATOR, TokenType.OPERATOR_AND },
            { Constants.OR_OPERATOR, TokenType.OPERATOR_OR },
            { Constants.ARROW_OPERATOR, TokenType.OPERATOR_ARROW },
            { Constants.EXCLAMATION.ToString(), TokenType.OPERATOR_NOT },
            { Constants.OPEN_BRACKET.ToString(), TokenType.PUNC_OPEN_BRACKET },
            { Constants.CLOSE_BRACKET.ToString(), TokenType.PUNC_CLOSE_BRACKET },
            { Constants.OPEN_CURLY_BRACKET.ToString(), TokenType.PUNC_OPEN_CURLY_BRACKET },
            { Constants.CLOSE_CURLY_BRACKET.ToString(), TokenType.PUNC_CLOSE_CURLY_BRACKET },
            { Constants.SEMICOLON.ToString(), TokenType.PUNC_SEMICOLON },
            { Constants.LOCK_KWORD, TokenType.ACTION_LOCK },
            { Constants.TRASH_KWORD, TokenType.ACTION_TRASH },
            { Constants.KEEP_KWORD, TokenType.ACTION_KEEP },
            { Constants.STAT_KWORD , TokenType.FUNCTION_STAT },
            { Constants.SUB_KWORD, TokenType.FUNCTION_SUB },
            { Constants.MAIN_KWORD, TokenType.FUNCTION_MAIN },
            { Constants.SLOT_KWORD, TokenType.FUNCTION_SLOT },
            { Constants.SET_KWORD, TokenType.FUNCTION_SET },
            { Constants.IF_KWORD, TokenType.KEYWORD_IF },
            { Constants.RETURN_KWORD, TokenType.KEYWORD_RETURN },
            { Constants.FLOWER_KWORD, TokenType.SLOT_FLOWER },
            { Constants.PLUME_KWORD, TokenType.SLOT_PLUME },
            { Constants.SANDS_KWORD, TokenType.SLOT_SANDS },
            { Constants.GOBLET_KWORD, TokenType.SLOT_GOBLET },
            { Constants.CIRCLET_KWORD, TokenType.SLOT_CIRCLET },
            { Constants.THREE_KWORD, TokenType.STAT_THREE },
            { Constants.FOUR_KWORD, TokenType.STAT_FOUR },
            { Constants.HP_KWORD, TokenType.STAT_HP },
            { Constants.HP_PER_KWORD, TokenType.STAT_HPP },
            { Constants.ATK_KWORD, TokenType.STAT_ATK },
            { Constants.ATK_PER_KWORD, TokenType.STAT_ATKP },
            { Constants.DEF_KWORD, TokenType.STAT_DEF },
            { Constants.DEF_PER_KWORD, TokenType.STAT_DEFP },
            { Constants.EM_KWORD, TokenType.STAT_EM },
            { Constants.ER_KWORD, TokenType.STAT_ER },
            { Constants.PYRO_KWORD, TokenType.STAT_PYRO },
            { Constants.CYRO_KWORD, TokenType.STAT_CRYO },
            { Constants.HYDRO_KWORD,  TokenType.STAT_HYDRO },
            { Constants.ELECTRO_KWORD, TokenType.STAT_ELECTRO },
            { Constants.DENDRO_KWORD, TokenType.STAT_DENDRO },
            { Constants.GEO_KWORD, TokenType.STAT_GEO },
            { Constants.ANEMO_KWORD, TokenType.STAT_ANEMO },
            { Constants.PHYSICAL_KWORD, TokenType.STAT_PHYSICAL },
            { Constants.CRITRATE_KWORD, TokenType.STAT_CRITRATE },
            { Constants.CRITDMG_KWORD, TokenType.STAT_CRITDMG },
            { Constants.HEALING_KWORD, TokenType.STAT_HEALING },
            { Constants.GLADIATOR_SET_KWORD, TokenType.SET_GLADIATOR },
            { Constants.WANDERER_SET_KWORD, TokenType.SET_WANDERER },
            { Constants.NOBLESSE_SET_KWORD, TokenType.SET_NOBLESSE },
            { Constants.BLOODSTAINED_SET_KWORD, TokenType.SET_BLOODSTAINED },
            { Constants.MAIDEN_BELOVED_SET_KWORD, TokenType.SET_MAIDEN },
            { Constants.VIRIDESCENT_SET_KWORD, TokenType.SET_VIRIDESCENT },
            { Constants.ARCHAIC_PETRA_SET_KWORD, TokenType.SET_ARCHAIC_PETRA },
            { Constants.RETRACING_BOLIDE_SET_KWORD, TokenType.SET_RETRACING_BOLIDE },
            { Constants.THUNDER_SOOTHER_SET_KWORD, TokenType.SET_THUNDER_SOOTHER },
            { Constants.THUNDERING_FURY_SET_KWORD, TokenType.SET_THUNDERING_FURY },
            { Constants.LAVAWALKER_SET_KWORD, TokenType.SET_LAVAWALKER },
            { Constants.CRIMSON_WITCH_SET_KWORD, TokenType.SET_CRIMSON_WITCH },
            { Constants.BLIZZARD_STRAYER_SET_KWORD, TokenType.SET_BLIZZARD_STRAYER },
            { Constants.HEART_OF_DEPTH_SET_KWORD, TokenType.SET_HEART_OF_DEPTH },
            { Constants.TENACITY_SET_KWORD, TokenType.SET_TENACITY },
            { Constants.PALEFLAME_SET_KWORD, TokenType.SET_PALEFLAME },
            { Constants.SHIMENAWA_SET_KWORD, TokenType.SET_SHIMENAWA },
            { Constants.EMBLEM_FATE_SET_KWORD, TokenType.SET_EMBLEM_FATE },
            { Constants.HUSK_SET_KWORD, TokenType.SET_HUSK },
            { Constants.OCEAN_HUE_SET_KWORD, TokenType.SET_OCEAN_HUE },
            { Constants.VERMILLION_SET_KWORD, TokenType.SET_VERMILLION },
            { Constants.ECHOES_SET_KWORD, TokenType.SET_ECHOES },
            { Constants.DEEPWOOD_SET_KWORD, TokenType.SET_DEEPWOOD },
            { Constants.GILDED_DREAMS_SET_KWORD, TokenType.SET_GILDED_DREAMS },
            { Constants.PAVILION_SET_KWORD, TokenType.SET_PAVILION },
            { Constants.LOST_PARADISE_SET_KWORD, TokenType.SET_LOST_PARADISE },
            { Constants.NYMPH_SET_KWORD, TokenType.SET_NYMPH },
            { Constants.VOURUKASHA_SET_KWORD, TokenType.SET_VOURUKASHA },
            { Constants.MARECHAUSSEE_SET_KWORD, TokenType.SET_MARECHAUSSEE },
            { Constants.GOLDEN_TROUPE_SET_KWORD, TokenType.SET_GOLDEN_TROUPE },
            { Constants.SONG_OF_DAYS_PAST_SET_KWORD, TokenType.SET_SONG_OF_DAYS_PAST },
            { Constants.ECHOING_WOODS_SET_KWORD, TokenType.SET_ECHOING_WOOD },
            { Constants.HARMONIC_WHIMSY_SET_KWORD, TokenType.SET_HARMONIC_WHIMSY },
            { Constants.UNFINISHED_REVERIE, TokenType.SET_UNFINISHED_REVERIE }
    };

        public Token MakeToken(string str, int lineNum)
        {
            if (tokenMap.ContainsKey(str))
            {
                return new Token(tokenMap[str], str, lineNum);
            }

            if (StringUtil.isAlphabets(str))
            {
                return new Token(TokenType.IDENTIFIER, str, lineNum);
            }

            throw new ParsingException(ErrorMessages.UnrecognizedToken, lineNum, str);
        }

        public List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            string tokenString = "";
            int lineNum = 1;
            bool isCommenting = false;

            foreach (char letter in input)
            {
                if (letter == Constants.HEX)
                {
                    isCommenting = true;
                }

                if (isCommenting)
                {
                    if (letter == Constants.NEW_LINE || letter == Constants.CARRIAGE_RETURN)
                    {
                        isCommenting = false;
                    }
                    else
                    {
                        continue;
                    }
                }


                if (StringUtil.isWhitespace(letter))
                {
                    if (!string.IsNullOrEmpty(tokenString))
                    {
                        tokens.Add(MakeToken(tokenString, lineNum));
                        tokenString = "";
                    }

                    if (letter == Constants.NEW_LINE)
                    {
                        lineNum++;
                    }
                    continue;
                }

                if (char.IsLetter(letter) || letter == Constants.PERCENT)
                {
                    if (StringUtil.isAlphaPercent(tokenString))
                    {
                        tokenString += letter;
                    }
                    else
                    {
                        tokens.Add(MakeToken(tokenString, lineNum));
                        tokenString = letter.ToString();
                    }
                    continue;
                }

                if (StringUtil.isCompoundOperator(tokenString + letter))
                {
                    tokens.Add(MakeToken(tokenString + letter, lineNum));
                    tokenString = "";
                    continue;
                }

                if (!string.IsNullOrEmpty(tokenString))
                {
                    tokens.Add(MakeToken(tokenString, lineNum));
                }

                tokenString = letter.ToString();
            }

            if (!string.IsNullOrEmpty(tokenString))
            {
                tokens.Add(MakeToken(tokenString, lineNum));
            }

            return tokens;
        }
    }
}