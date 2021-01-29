namespace AdvancedAutoResolve.Models
{
    internal struct PartyLeader
    {
        internal PartyLeader(int leadershipLevel, int tacticsLevel,
            bool hasTF, bool hasLF, bool hasAW, bool hasPE, bool hasLW, bool hasC, bool hasO, bool hasER, bool hasE, bool hasPB, bool hasB, bool hasCO, bool hasV, bool hasR)
        {
            LeadershipLevel = leadershipLevel;
            TacticsLevel = tacticsLevel;

            HasTightFormationsPerk = hasTF;
            HasLooseFormationsPerk = hasLF;
            HasAsymmetricalWarfarePerk = hasAW;
            HasProperEngagementPerk = hasPE;
            HasLawKeeperPerk = hasLW;
            HasCoachingPerk = hasC;
            HasOnTheMarchPerk = hasO;
            HasEliteReservesPerk = hasER;
            HasEncirclementPerk = hasE;
            HasPreBattleManeuversPerk = hasPB;
            HasBeseigedPerk = hasB;
            HasCounteroffensivePerk = hasCO;
            HasVanguardPerk = hasV;
            HasRearguardPerk = hasR;
        }

        internal int LeadershipLevel { get; }
        internal int TacticsLevel { get; }
        internal bool HasTightFormationsPerk { get; }
        internal bool HasLooseFormationsPerk { get; }
        internal bool HasAsymmetricalWarfarePerk { get; }
        internal bool HasProperEngagementPerk { get; }
        internal bool HasLawKeeperPerk { get; }
        internal bool HasCoachingPerk { get; }
        internal bool HasOnTheMarchPerk { get; }
        internal bool HasEliteReservesPerk { get; }
        internal bool HasEncirclementPerk { get; }
        internal bool HasPreBattleManeuversPerk { get; }
        internal bool HasBeseigedPerk { get; }
        internal bool HasCounteroffensivePerk { get; }
        internal bool HasVanguardPerk { get; }
        internal bool HasRearguardPerk { get; }
    }
}
