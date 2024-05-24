using ArtifactSelector.source_processor;
using System.Collections.Generic;

namespace ArtifactSelector.model
{
    internal static class MainStatDictionary
    {
        private readonly static Dictionary<string, MainStat> scannerDictionary = new Dictionary<string, MainStat>
        {
            {"hp", MainStat.Hp_},
            {"atk", MainStat.Atk_},
            {"def", MainStat.Def_},
            {"elementalmastery", MainStat.Em},
            {"energyrecharge", MainStat.Er},
            {"pyrodmgbonus", MainStat.Pyro},
            {"cryodmgbonus", MainStat.Cryo},
            {"hydrodmgbonus", MainStat.Hydro},
            {"electrodmgbonus", MainStat.Electro},
            {"dendrodmgbonus", MainStat.Dendro},
            {"geodmgbonus", MainStat.Geo},
            {"anemodmgbonus", MainStat.Anemo},
            {"physicaldmgbonus", MainStat.Physical},
            {"critrate", MainStat.CritRate},
            {"critdmg", MainStat.CritDmg},
            {"healingbonus", MainStat.Healing},
        };

        private readonly static Dictionary<TokenType, MainStat> tokenDictionary = new Dictionary<TokenType, MainStat>
        {
            {TokenType.STAT_HP, MainStat.Hp},
            {TokenType.STAT_HPP, MainStat.Hp_},
            {TokenType.STAT_ATK, MainStat.Atk},
            {TokenType.STAT_ATKP, MainStat.Atk_},
            {TokenType.STAT_DEF, MainStat.Def},
            {TokenType.STAT_DEFP, MainStat.Def_},
            {TokenType.STAT_EM, MainStat.Em},
            {TokenType.STAT_ER, MainStat.Er},
            {TokenType.STAT_PYRO, MainStat.Pyro},
            {TokenType.STAT_CRYO, MainStat.Cryo},
            {TokenType.STAT_HYDRO, MainStat.Hydro},
            {TokenType.STAT_ELECTRO, MainStat.Electro},
            {TokenType.STAT_DENDRO, MainStat.Dendro},
            {TokenType.STAT_GEO, MainStat.Geo},
            {TokenType.STAT_ANEMO, MainStat.Anemo},
            {TokenType.STAT_PHYSICAL, MainStat.Physical},
            {TokenType.STAT_CRITRATE, MainStat.CritRate},
            {TokenType.STAT_CRITDMG, MainStat.CritDmg},
            {TokenType.STAT_HEALING, MainStat.Healing},
        };

        public static MainStat FromScanner(string str)
        {
            if (scannerDictionary.TryGetValue(str, out MainStat res))
            {
                return res;
            }

            HashSet<string> keys = new HashSet<string>(scannerDictionary.Keys);
            string closest = StringUtil.FindClosestInList(str, keys, 90);
            return scannerDictionary.TryGetValue(closest, out res) ? res : MainStat.None;
        }

        public static MainStat FromToken(TokenType token)
        {
            return tokenDictionary.TryGetValue(token, out MainStat res) ? res : MainStat.None;
        }
    }
}
