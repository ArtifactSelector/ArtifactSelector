using System.Collections.Generic;

namespace ArtifactSelector.source_processor
{
    public enum TokenType
    {
        IDENTIFIER,
        OPERATOR_EQUAL,
        OPERATOR_AND,
        OPERATOR_OR,
        OPERATOR_NOT,
        OPERATOR_ARROW,
        PUNC_OPEN_BRACKET,
        PUNC_CLOSE_BRACKET,
        PUNC_OPEN_CURLY_BRACKET,
        PUNC_CLOSE_CURLY_BRACKET,
        PUNC_SEMICOLON,
        ACTION_LOCK,
        ACTION_TRASH,
        ACTION_KEEP,
        FUNCTION_STAT,
        FUNCTION_SUB,
        FUNCTION_MAIN,
        FUNCTION_SLOT,
        FUNCTION_SET,
        KEYWORD_IF,
        KEYWORD_RETURN,
        SLOT_FLOWER,
        SLOT_PLUME,
        SLOT_SANDS,
        SLOT_GOBLET,
        SLOT_CIRCLET,
        STAT_THREE,
        STAT_FOUR,
        STAT_HP,
        STAT_HPP,
        STAT_ATK,
        STAT_ATKP,
        STAT_DEF,
        STAT_DEFP,
        STAT_EM,
        STAT_ER,
        STAT_PYRO,
        STAT_CRYO,
        STAT_HYDRO,
        STAT_ELECTRO,
        STAT_DENDRO,
        STAT_GEO,
        STAT_ANEMO,
        STAT_PHYSICAL,
        STAT_CRITRATE,
        STAT_CRITDMG,
        STAT_HEALING,
        SET_GLADIATOR,
        SET_WANDERER,
        SET_NOBLESSE,
        SET_BLOODSTAINED,
        SET_MAIDEN,
        SET_VIRIDESCENT,
        SET_ARCHAIC_PETRA,
        SET_RETRACING_BOLIDE,
        SET_THUNDER_SOOTHER,
        SET_THUNDERING_FURY,
        SET_LAVAWALKER,
        SET_CRIMSON_WITCH,
        SET_BLIZZARD_STRAYER,
        SET_HEART_OF_DEPTH,
        SET_TENACITY,
        SET_PALEFLAME,
        SET_SHIMENAWA,
        SET_EMBLEM_FATE,
        SET_HUSK,
        SET_OCEAN_HUE,
        SET_VERMILLION,
        SET_ECHOES,
        SET_DEEPWOOD,
        SET_GILDED_DREAMS,
        SET_PAVILION,
        SET_LOST_PARADISE,
        SET_NYMPH,
        SET_VOURUKASHA,
        SET_MARECHAUSSEE,
        SET_GOLDEN_TROUPE,
        SET_SONG_OF_DAYS_PAST,
        SET_ECHOING_WOOD,
        SET_HARMONIC_WHIMSY,
        SET_UNFINISHED_REVERIE
    }

    public struct Token
    {
        private readonly TokenType tokenType;
        private readonly string tokenValue;
        private readonly int lineNum;
        private static readonly HashSet<TokenType> FunctionTokens = new HashSet<TokenType> {
        TokenType.FUNCTION_STAT,
        TokenType.FUNCTION_SUB,
        TokenType.FUNCTION_MAIN,
        TokenType.FUNCTION_SLOT,
        TokenType.FUNCTION_SET,
        };

        public Token(TokenType type, string value, int lineNum)
        {
            this.tokenType = type;
            this.tokenValue = value;
            this.lineNum = lineNum;
        }

        public TokenType TokenType => tokenType;

        public string TokenValue => tokenValue;

        public int LineNum => lineNum;

        public bool IsFunction()
        {
            return FunctionTokens.Contains(tokenType);
        }
        public override string ToString()
        {
            return tokenValue;
        }

    }
}
