using ArtifactSelector.source_processor;
using System.Collections.Generic;

namespace ArtifactSelector.model
{
    internal static class GearSlotDictionary
    {
        private readonly static Dictionary<string, GearSlot> scannerDictionary = new Dictionary<string, GearSlot>
        {
            {"flower", GearSlot.Flower},
            {"plume", GearSlot.Plume},
            {"sands", GearSlot.Sands},
            {"goblet", GearSlot.Goblet},
            {"circlet", GearSlot.Circlet},
        };

        private readonly static Dictionary<TokenType, GearSlot> tokenDictionary = new Dictionary<TokenType, GearSlot>
        {
            {TokenType.SLOT_FLOWER, GearSlot.Flower},
            {TokenType.SLOT_PLUME, GearSlot.Plume},
            {TokenType.SLOT_SANDS, GearSlot.Sands},
            {TokenType.SLOT_GOBLET, GearSlot.Goblet},
            {TokenType.SLOT_CIRCLET, GearSlot.Circlet},
        };

        public static GearSlot FromScanner(string str)
        {
            if (scannerDictionary.TryGetValue(str, out GearSlot res))
            {
                return res;
            }

            HashSet<string> keys = new HashSet<string>(scannerDictionary.Keys);
            string closest = StringUtil.FindClosestInList(str, keys, 90);
            return scannerDictionary.TryGetValue(closest, out res) ? res : GearSlot.None;
        }

        public static GearSlot FromToken(TokenType token)
        {
            return tokenDictionary.TryGetValue(token, out GearSlot res) ? res : GearSlot.None;
        }
    }
}
