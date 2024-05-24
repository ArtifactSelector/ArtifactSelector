using ArtifactSelector.source_processor;
using System.Collections.Generic;

namespace ArtifactSelector.model
{
    internal static class SubStatDictionary
    {
        private readonly static Dictionary<string, SubStat> scannerDictionary = new Dictionary<string, SubStat>
        {
            {"hp", SubStat.Hp},
            {"hp%", SubStat.Hp_},
            {"atk", SubStat.Atk},
            {"atk%", SubStat.Atk_},
            {"def", SubStat.Def},
            {"def%", SubStat.Def_},
            {"elementalmastery", SubStat.Em},
            {"energyrecharge%", SubStat.Er},
            {"critrate%", SubStat.CritRate},
            {"critdmg%", SubStat.CritDmg},
        };

        private readonly static Dictionary<TokenType, SubStat> tokenDictionary = new Dictionary<TokenType, SubStat>
        {
            {TokenType.STAT_HP, SubStat.Hp},
            {TokenType.STAT_HPP, SubStat.Hp_},
            {TokenType.STAT_ATK, SubStat.Atk},
            {TokenType.STAT_ATKP, SubStat.Atk_},
            {TokenType.STAT_DEF, SubStat.Def},
            {TokenType.STAT_DEFP, SubStat.Def_},
            {TokenType.STAT_EM, SubStat.Em},
            {TokenType.STAT_ER, SubStat.Er},
            {TokenType.STAT_CRITRATE, SubStat.CritRate},
            {TokenType.STAT_CRITDMG, SubStat.CritDmg},
        };

        public static SubStat FromScanner(string str)
        {
            if (scannerDictionary.TryGetValue(str, out SubStat res))
            {
                return res;
            }

            HashSet<string> keys = new HashSet<string>(scannerDictionary.Keys);
            string closest = StringUtil.FindClosestInList(str, keys, 90);
            return scannerDictionary.TryGetValue(closest, out res) ? res : SubStat.None;
        }

        public static SubStat FromToken(TokenType token)
        {
            return tokenDictionary.TryGetValue(token, out SubStat res) ? res : SubStat.None;
        }
    }
}
