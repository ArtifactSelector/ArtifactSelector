using ArtifactSelector.source_processor;
using System.Collections.Generic;

namespace ArtifactSelector.model
{
    internal static class ArtifactSetDictionary
    {
        private readonly static Dictionary<string, ArtifactSet> scannerDictionary = new Dictionary<string, ArtifactSet>
        {
            {"gladiatorsfinale", ArtifactSet.GladiatorsFinale},
            {"wandererstroupe", ArtifactSet.WanderersTroupe},
            {"noblesseoblige", ArtifactSet.NoblesseOblige},
            {"bloodstainedchivalry", ArtifactSet.BloodstainedChivalry},
            {"maidenbeloved", ArtifactSet.MaidenBeloved},
            {"viridescentvenerer", ArtifactSet.ViridescentVenerer},
            {"archaicpetra", ArtifactSet.ArchaicPetra},
            {"retracingbolide", ArtifactSet.RetracingBolide},
            {"thundersoother", ArtifactSet.Thundersoother},
            {"thunderingfury", ArtifactSet.ThunderingFury},
            {"lavawalker", ArtifactSet.Lavawalker},
            {"crimsonwitchofflames", ArtifactSet.CrimsonWitchOfFlames},
            {"blizzardstrayer", ArtifactSet.BlizzardStrayer},
            {"heartofdepth", ArtifactSet.HeartOfDepth},
            {"tenacityofthemillelith", ArtifactSet.TenacityOfTheMillelith},
            {"paleflame", ArtifactSet.PaleFlame},
            {"shimenawareminiscence", ArtifactSet.ShimenawasReminiscence},
            {"emblemofseveredfate", ArtifactSet.EmblemOfSeveredFate},
            {"huskopulentdreams", ArtifactSet.HuskOfOpulentDreams},
            {"oceanhuedclam", ArtifactSet.OceanHuedClam},
            {"vermillionhereafter", ArtifactSet.VermillionHereafter},
            {"echoesofanoffering", ArtifactSet.EchoesOfAnOffering},
            {"deepwoodmemories", ArtifactSet.DeepwoodMemories},
            {"gildeddreams", ArtifactSet.GildedDreams},
            {"desertpavilionchronicle", ArtifactSet.DesertPavilionChronicle},
            {"flowerofparadiselost", ArtifactSet.FlowerOfParadiseLost},
            {"nymphsdream", ArtifactSet.NymphsDream},
            {"vourukashasglow", ArtifactSet.VourukashasGlow},
            {"marechausseehunter", ArtifactSet.MarechausseeHunter},
            {"goldentroupe", ArtifactSet.GoldenTroupe},
            {"songofdayspast", ArtifactSet.SongOfDaysPast},
            {"nighttimewhispersintheechoingwoods" , ArtifactSet.NighttimeWhispersInTheEchoingWoods},
            {"fragmentofharmonicwhimsy" , ArtifactSet.FragmentOfHarmonicWhimsy},
            {"unfinishedreverie" , ArtifactSet.UnfinishedReverie},
        };

        private readonly static Dictionary<TokenType, ArtifactSet> tokenDictionary = new Dictionary<TokenType, ArtifactSet>
        {
            {TokenType.SET_GLADIATOR, ArtifactSet.GladiatorsFinale},
            {TokenType.SET_WANDERER, ArtifactSet.WanderersTroupe},
            {TokenType.SET_NOBLESSE, ArtifactSet.NoblesseOblige},
            {TokenType.SET_BLOODSTAINED, ArtifactSet.BloodstainedChivalry},
            {TokenType.SET_MAIDEN, ArtifactSet.MaidenBeloved},
            {TokenType.SET_VIRIDESCENT, ArtifactSet.ViridescentVenerer},
            {TokenType.SET_ARCHAIC_PETRA, ArtifactSet.ArchaicPetra},
            {TokenType.SET_RETRACING_BOLIDE, ArtifactSet.RetracingBolide},
            {TokenType.SET_THUNDER_SOOTHER, ArtifactSet.Thundersoother},
            {TokenType.SET_THUNDERING_FURY, ArtifactSet.ThunderingFury},
            {TokenType.SET_LAVAWALKER, ArtifactSet.Lavawalker},
            {TokenType.SET_CRIMSON_WITCH, ArtifactSet.CrimsonWitchOfFlames},
            {TokenType.SET_BLIZZARD_STRAYER, ArtifactSet.BlizzardStrayer},
            {TokenType.SET_HEART_OF_DEPTH, ArtifactSet.HeartOfDepth},
            {TokenType.SET_TENACITY, ArtifactSet.TenacityOfTheMillelith},
            {TokenType.SET_PALEFLAME, ArtifactSet.PaleFlame},
            {TokenType.SET_SHIMENAWA, ArtifactSet.ShimenawasReminiscence},
            {TokenType.SET_EMBLEM_FATE, ArtifactSet.EmblemOfSeveredFate},
            {TokenType.SET_HUSK, ArtifactSet.HuskOfOpulentDreams},
            {TokenType.SET_OCEAN_HUE, ArtifactSet.OceanHuedClam},
            {TokenType.SET_VERMILLION, ArtifactSet.VermillionHereafter},
            {TokenType.SET_ECHOES, ArtifactSet.EchoesOfAnOffering},
            {TokenType.SET_DEEPWOOD, ArtifactSet.DeepwoodMemories},
            {TokenType.SET_GILDED_DREAMS, ArtifactSet.GildedDreams},
            {TokenType.SET_PAVILION, ArtifactSet.DesertPavilionChronicle},
            {TokenType.SET_LOST_PARADISE, ArtifactSet.FlowerOfParadiseLost},
            {TokenType.SET_NYMPH, ArtifactSet.NymphsDream},
            {TokenType.SET_VOURUKASHA, ArtifactSet.VourukashasGlow},
            {TokenType.SET_MARECHAUSSEE, ArtifactSet.MarechausseeHunter},
            {TokenType.SET_GOLDEN_TROUPE, ArtifactSet.GoldenTroupe},
            {TokenType.SET_SONG_OF_DAYS_PAST, ArtifactSet.SongOfDaysPast},
            {TokenType.SET_ECHOING_WOOD, ArtifactSet.NighttimeWhispersInTheEchoingWoods},
            {TokenType.SET_HARMONIC_WHIMSY, ArtifactSet.FragmentOfHarmonicWhimsy},
            {TokenType.SET_UNFINISHED_REVERIE, ArtifactSet.UnfinishedReverie},
        };

        public static ArtifactSet FromScanner(string str)
        {
            if (scannerDictionary.TryGetValue(str, out ArtifactSet res))
            {
                return res;
            }

            HashSet<string> keys = new HashSet<string>(scannerDictionary.Keys);
            string closest = StringUtil.FindClosestInList(str, keys, 80);
            return scannerDictionary.TryGetValue(closest, out res) ? res : ArtifactSet.None;
        }

        public static ArtifactSet FromToken(TokenType token)
        {
            return tokenDictionary.TryGetValue(token, out ArtifactSet res) ? res : ArtifactSet.None;
        }
    }
}
