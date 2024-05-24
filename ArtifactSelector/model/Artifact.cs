using System.Collections.Generic;

namespace ArtifactSelector.model
{
    public enum MainStat
    {
        Hp,
        Hp_,
        Atk,
        Atk_,
        Def,
        Def_,
        Em,
        Er,
        Pyro,
        Cryo,
        Hydro,
        Electro,
        Dendro,
        Geo,
        Anemo,
        Physical,
        CritRate,
        CritDmg,
        Healing,
        None
    }

    public enum SubStat
    {
        Hp,
        Hp_,
        Atk,
        Atk_,
        Def,
        Def_,
        Em,
        Er,
        CritRate,
        CritDmg,
        None
    }

    public enum ArtifactSet
    {
        GladiatorsFinale,
        WanderersTroupe,
        NoblesseOblige,
        BloodstainedChivalry,
        MaidenBeloved,
        ViridescentVenerer,
        ArchaicPetra,
        RetracingBolide,
        Thundersoother,
        ThunderingFury,
        Lavawalker,
        CrimsonWitchOfFlames,
        BlizzardStrayer,
        HeartOfDepth,
        TenacityOfTheMillelith,
        PaleFlame,
        ShimenawasReminiscence,
        EmblemOfSeveredFate,
        HuskOfOpulentDreams,
        OceanHuedClam,
        VermillionHereafter,
        EchoesOfAnOffering,
        DeepwoodMemories,
        GildedDreams,
        DesertPavilionChronicle,
        FlowerOfParadiseLost,
        NymphsDream,
        VourukashasGlow,
        MarechausseeHunter,
        GoldenTroupe,
        SongOfDaysPast,
        NighttimeWhispersInTheEchoingWoods,
        FragmentOfHarmonicWhimsy,
        UnfinishedReverie,
        None
    }

    public enum GearSlot
    {
        Flower,
        Plume,
        Sands,
        Goblet,
        Circlet,
        None
    }

    public class Artifact
    {
        private readonly GearSlot slot;
        private readonly ArtifactSet artifactSet;
        private readonly MainStat mainStat;
        private readonly List<SubStat> subStats;

        public GearSlot Slot
        {
            get { return slot; }
        }

        public ArtifactSet Set
        {
            get { return artifactSet; }
        }

        public MainStat Main
        {
            get { return mainStat; }
        }

        public bool HasSubStat(SubStat substat)
        {
            return substat != SubStat.None && subStats.Contains(substat);
        }

        public Artifact(GearSlot slot, ArtifactSet artifactSet, MainStat mainStat, List<SubStat> subStats)
        {
            this.slot = slot;
            this.artifactSet = artifactSet;
            this.mainStat = mainStat;
            this.subStats = subStats;
        }

        public override string ToString()
        {
            return $"Artifact: Slot = {slot}, Artifact Set = {artifactSet}, Main Stat = {mainStat}, Substats = {string.Join(", ", subStats)}";
        }

        public bool isValid()
        {
            bool isSubStatOk = subStats.Count > 2 && !subStats.Contains(SubStat.None);

            if (slot == GearSlot.None || artifactSet == ArtifactSet.None || mainStat == MainStat.None || !isSubStatOk)
            {
                return false;
            }

            return true;
        }

        public bool HasThreeSub()
        {
            return isValid() && subStats.Count == 3;
        }

        public bool HasFourSub()
        {
            return isValid() && subStats.Count == 4;
        }
    }
}
