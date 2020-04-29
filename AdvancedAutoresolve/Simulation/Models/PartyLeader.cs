﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAutoResolve.Simulation.Models
{
    internal struct PartyLeader
    {
        internal PartyLeader(int leadershipLevel, int tacticsLevel, bool hasTacticalSuperiorityPerk, bool hasHammerAndAnvilPerk, bool hasPhalanxPerk)
        {
            LeadershipLevel = leadershipLevel;
            TacticsLevel = tacticsLevel;
            HasTacticalSuperiorityPerk = hasTacticalSuperiorityPerk;
            HasHammerAndAnvilPerk = hasHammerAndAnvilPerk;
            HasPhalanxPerk = hasPhalanxPerk;
        }

        internal int LeadershipLevel { get; }
        internal int TacticsLevel { get; }
        internal bool HasTacticalSuperiorityPerk { get; }
        internal bool HasHammerAndAnvilPerk { get; }
        internal bool HasPhalanxPerk { get; }
    }
}
