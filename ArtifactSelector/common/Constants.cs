using System.Collections.Generic;

namespace ArtifactSelector
{
    internal static class Constants
    {
        // White spaces
        public const char TAB = '\t';
        public const char NEW_LINE = '\n';
        public const char CARRIAGE_RETURN = '\r';
        public const char SPACE = ' ';
        public readonly static HashSet<char> WHITESPACES = new HashSet<char> { TAB, NEW_LINE, CARRIAGE_RETURN, SPACE };

        // Symbols
        public const char EQUAL = '=';
        public const char AMPERSAND = '&';
        public const char BAR = '|';
        public const char MORETHAN = '>';
        public const char OPEN_BRACKET = '(';
        public const char CLOSE_BRACKET = ')';
        public const char OPEN_CURLY_BRACKET = '{';
        public const char CLOSE_CURLY_BRACKET = '}';
        public const char SEMICOLON = ';';
        public const char PERCENT = '%';
        public const char EXCLAMATION = '!';
        public const char HEX = '#';

        // Compound Operators
        public const string AND_OPERATOR = "&&";
        public const string OR_OPERATOR = "||";
        public const string ARROW_OPERATOR = "=>";
        public readonly static HashSet<string> COMPOUND_OPERATORS = new HashSet<string> { AND_OPERATOR, OR_OPERATOR, ARROW_OPERATOR };

        // Action Keywords
        public const string KEEP_KWORD = "Keep";
        public const string LOCK_KWORD = "Lock";
        public const string TRASH_KWORD = "Trash";

        // Specific Keywords
        public const string RETURN_KWORD = "return";
        public const string IF_KWORD = "if";

        // Function Keywords
        public const string SET_KWORD = "Set";
        public const string SLOT_KWORD = "Slot";
        public const string STAT_KWORD = "Stat";
        public const string MAIN_KWORD = "Main";
        public const string SUB_KWORD = "Sub";

        // Slot Keywords
        public const string FLOWER_KWORD = "FLOWER";
        public const string PLUME_KWORD = "PLUME";
        public const string SANDS_KWORD = "SANDS";
        public const string GOBLET_KWORD = "GOBLET";
        public const string CIRCLET_KWORD = "CIRCLET";

        // Other Keyowords
        public const string THREE_KWORD = "THREE";
        public const string FOUR_KWORD = "FOUR";
        public const string NONE_KWORD = "None";

        // Stats Keywords
        public const string HP_KWORD = "HP";
        public const string HP_PER_KWORD = "HP%";
        public const string ATK_KWORD = "ATK";
        public const string ATK_PER_KWORD = "ATK%";
        public const string DEF_KWORD = "DEF";
        public const string DEF_PER_KWORD = "DEF%";
        public const string EM_KWORD = "EM";
        public const string ER_KWORD = "ER";
        public const string PYRO_KWORD = "PYRO";
        public const string CYRO_KWORD = "CRYO";
        public const string HYDRO_KWORD = "HYDRO";
        public const string ELECTRO_KWORD = "ELECTRO";
        public const string DENDRO_KWORD = "DENDRO";
        public const string GEO_KWORD = "GEO";
        public const string ANEMO_KWORD = "ANEMO";
        public const string PHYSICAL_KWORD = "PHYSICAL";
        public const string CRITRATE_KWORD = "CRITRATE";
        public const string CRITDMG_KWORD = "CRITDMG";
        public const string HEALING_KWORD = "HEALING";

        // Artifact Set Keywords
        public const string GLADIATOR_SET_KWORD = "GLADIATORSFINALE";
        public const string WANDERER_SET_KWORD = "WANDERERSTROUPE";
        public const string NOBLESSE_SET_KWORD = "NOBLESSEOBLIGE";
        public const string BLOODSTAINED_SET_KWORD = "BLOODSTAINEDCHIVALRY";
        public const string MAIDEN_BELOVED_SET_KWORD = "MAIDENBELOVED";
        public const string VIRIDESCENT_SET_KWORD = "VIRIDESCENTVENERER";
        public const string ARCHAIC_PETRA_SET_KWORD = "ARCHAICPETRA";
        public const string RETRACING_BOLIDE_SET_KWORD = "RETRACINGBOLIDE";
        public const string THUNDER_SOOTHER_SET_KWORD = "THUNDERSOOTHER";
        public const string THUNDERING_FURY_SET_KWORD = "THUNDERINGFURY";
        public const string LAVAWALKER_SET_KWORD = "LAVAWALKER";
        public const string CRIMSON_WITCH_SET_KWORD = "CRIMSONWITCHOFFLAMES";
        public const string BLIZZARD_STRAYER_SET_KWORD = "BLIZZARDSTRAYER";
        public const string HEART_OF_DEPTH_SET_KWORD = "HEARTOFDEPTH";
        public const string TENACITY_SET_KWORD = "TENACITYOFTHEMILLELITH";
        public const string PALEFLAME_SET_KWORD = "PALEFLAME";
        public const string SHIMENAWA_SET_KWORD = "SHIMENAWASREMINISCENCE";
        public const string EMBLEM_FATE_SET_KWORD = "EMBLEMOFSEVEREDFATE";
        public const string HUSK_SET_KWORD = "HUSKOFPOLENTDREAMS";
        public const string OCEAN_HUE_SET_KWORD = "OCEANHUEDCLAM";
        public const string VERMILLION_SET_KWORD = "VERMILLIONHEREAFTER";
        public const string ECHOES_SET_KWORD = "ECHOESOFANOFFERING";
        public const string DEEPWOOD_SET_KWORD = "DEEPWOODMEMORIES";
        public const string GILDED_DREAMS_SET_KWORD = "GILDEDDREAMS";
        public const string PAVILION_SET_KWORD = "DESERTPAVILIONCHRONICLE";
        public const string LOST_PARADISE_SET_KWORD = "FLOWEROFPARADISELOST";
        public const string NYMPH_SET_KWORD = "NYMPHSDREAM";
        public const string VOURUKASHA_SET_KWORD = "VOURUKASHASGLOW";
        public const string MARECHAUSSEE_SET_KWORD = "MARECHAUSSEEHUNTER";
        public const string GOLDEN_TROUPE_SET_KWORD = "GOLDENTROUPE";
        public const string SONG_OF_DAYS_PAST_SET_KWORD = "SONGOFDAYSPAST";
        public const string ECHOING_WOODS_SET_KWORD = "NIGHTTIMEWHISPERSINTHEECHOINGWOODS";
        public const string HARMONIC_WHIMSY_SET_KWORD = "FRAGMENTOFHARMONICWHIMSY";
        public const string UNFINISHED_REVERIE = "UNFINISHEDREVERIE";
    }
}
