namespace AdvancedAutoResolve.Models
{
    internal struct PartyLeader
    {
        internal PartyLeader(int leadershipLevel, int tacticsLevel,
            bool hasTF, bool hasLF, bool hasAW, bool hasPE, bool hasLW, bool hasC, bool hasER, bool hasE, bool hasCO, bool hasB, bool hasV, bool hasR)
        {
            LeadershipLevel = leadershipLevel;
            TacticsLevel = tacticsLevel;

            HasTightFormationsPerk = hasTF;
            HasLooseFormationsPerk = hasLF;
            HasAsymmetricalWarfarePerk = hasAW;
            HasProperEngagementPerk = hasPE;
            HasLawKeeperPerk = hasLW;
            HasCoachingPerk = hasC;
            HasEliteReservesPerk = hasER;
            HasEncirclementPerk = hasE;
            HasCounteroffensivePerk = hasCO;
            HasBeseigedPerk = hasB;
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
        internal bool HasEliteReservesPerk { get; }
        internal bool HasEncirclementPerk { get; }
        internal bool HasCounteroffensivePerk { get; }
        internal bool HasBeseigedPerk { get; }
        internal bool HasVanguardPerk { get; }
        internal bool HasRearguardPerk { get; }
    }
}
