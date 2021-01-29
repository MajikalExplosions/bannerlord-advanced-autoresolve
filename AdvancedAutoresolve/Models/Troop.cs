using AdvancedAutoResolve.Configuration;
using AdvancedAutoResolve.Simulation;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace AdvancedAutoResolve.Models
{
    internal class Troop
    {
        internal Troop(CharacterObject characterObject, Party partyModel, TroopType troopType)
        {
            CharacterObject = characterObject;
            Health = CharacterObject.MaxHitPoints();
            PartyModel = partyModel;
            TroopType = troopType;
        }

        internal CharacterObject CharacterObject { get; }
        internal int Health { get; set; }
        internal Party PartyModel { get; }
        internal TroopType TroopType { get; }

        public bool IsInfantry
        {
            get
            {
                return TroopType == TroopType.ShockInfantry || TroopType == TroopType.ShockInfantry || TroopType == TroopType.ShockInfantry;
            }
        }

        public bool IsRanged
        {
            get
            {
                return TroopType == TroopType.Ranged || TroopType == TroopType.HorseArcher;
            }
        }

        public bool IsMounted
        {
            get
            {
                return TroopType == TroopType.LightCavalry || TroopType == TroopType.HeavyCavalry || TroopType == TroopType.HorseArcher;
            }
        }

        /// <summary>
        /// Subtracts <paramref name="damage"/> amount from <see cref="Troop.Health"/>
        /// </summary>
        /// <param name="damage">damage amount</param>
        /// <returns><c>true</c> if <see cref="Troop.Health"/> is 0 or below</returns>
        internal bool ApplyDamage(int damage)
        {
            Health -= damage;
            return Health <= 0;
        }

        internal Modifiers GetModifiersFromTactics()
        {
            switch (TroopType)
            {
                case TroopType.ShockInfantry:
                case TroopType.SkirmishInfantry:
                case TroopType.HeavyInfantry:
                    return PartyModel.CurrentInfantryTactic.Modifiers;
                case TroopType.Ranged:
                    return PartyModel.CurrentRangedTactic.Modifiers;
                case TroopType.LightCavalry:
                case TroopType.HeavyCavalry:
                    return PartyModel.CurrentCavalryTactic.Modifiers;
                case TroopType.HorseArcher:
                    return PartyModel.CurrentHorseArchersTactic.Modifiers;
                default:
                    throw new NotImplementedException($"Not supported TroopType {TroopType}");
            }
        }

        /// <summary>
        /// Vanilla troop power plus 5% with each troop tier (up to 30% at T6)
        /// </summary>
        internal float GetPower()
        {
            var basePower = CharacterObject.GetPower();
            var tier = (float)CharacterObject.Tier;
            var finalPower = basePower + tier / 20f; // 5% extra power per unit tier
            return finalPower;
        }

        /// <summary>
        /// Decide whether the defender would be attacked at this point in time, or not.
        /// </summary>
        internal bool DoesItMakeSenseToAttackThisUnit(Troop defender)
        {
            if (TroopType == TroopType.HeavyInfantry)
            {
                if (defender.TroopType == TroopType.Ranged
                    && defender.PartyModel.CurrentRangedTactic == Config.CurrentConfig.Tactics.Find(t => t.Name == "SkirmishBehindInfantry")
                    && defender.PartyModel.HasInfantry)
                {
                    // attacker is infantry, and the defender is an archer in skirmish tactic and his party still has infantry to cower behind.
                    return false;
                }
            }
            return true;
        }

        internal float GetDefenseModifierFromLeader()
        {
            float modifier = 1f;

            if (PartyModel.HasLeader)
            {
                modifier += PartyModel.PartyLeader.TacticsLevel * (Config.CurrentConfig.PartyLeaderModifiers.TacticsModifiers.DefenseBonus - 1f) / 100;
                modifier += PartyModel.PartyLeader.LeadershipLevel * (Config.CurrentConfig.PartyLeaderModifiers.LeadershipModifiers.DefenseBonus - 1f) / 100;
            }

            return modifier;
        }

        internal float GetAttackModifierFromLeader()
        {
            float modifier = 1f;

            if (PartyModel.HasLeader)
            {
                modifier += PartyModel.PartyLeader.TacticsLevel * (Config.CurrentConfig.PartyLeaderModifiers.TacticsModifiers.AttackBonus - 1f) / 100;
                modifier += PartyModel.PartyLeader.LeadershipLevel * (Config.CurrentConfig.PartyLeaderModifiers.LeadershipModifiers.AttackBonus -1f) / 100;
            }

            return modifier;
        }

        internal Modifiers GetSiegeDefenderModifiers()
        {
            return PartyModel.IsSiegeDefender ? Config.CurrentConfig.SiegeDefendersModifiers : Modifiers.GetDefaultModifiers();
        }

        internal float GetAttackingModifierFromLeaderPerks(Troop defender, TerrainType terrain, bool isInitiator, MapEvent battle, PartyBase defendingParty)
        {
            if (!PartyModel.HasLeader) return 1f;

            float modifier = 1f;

            if (PartyModel.PartyLeader.HasTightFormationsPerk && IsInfantry && defender.IsMounted)
            {
                modifier += 0.1f;
            }

            if (PartyModel.PartyLeader.HasAsymmetricalWarfarePerk && (terrain == TerrainType.Snow || terrain == TerrainType.Forest))
            {
                modifier += 0.1f;
            }

            if (PartyModel.PartyLeader.HasProperEngagementPerk && (terrain == TerrainType.Plain || terrain == TerrainType.Steppe || terrain == TerrainType.Desert))
            {
                modifier += 0.05f;
            }

            if (PartyModel.PartyLeader.HasLawKeeperPerk && defendingParty.IsMobile && defendingParty.MobileParty.IsBandit)
            {
                modifier += 0.1f;
            }

            if (PartyModel.PartyLeader.HasCoachingPerk)
            {
                modifier += 0.03f;
            }

            if (PartyModel.PartyLeader.HasEncirclementPerk && PartyModel.Troops.Count > defender.PartyModel.Troops.Count)
            {
                modifier += 0.05f;
            }

            if (PartyModel.PartyLeader.HasCounteroffensivePerk && PartyModel.Troops.Count < defender.PartyModel.Troops.Count)
            {
                modifier += 0.1f;
            }

            if (PartyModel.PartyLeader.HasBeseigedPerk && ! isInitiator && battle.IsSiegeAssault)
            {
                modifier += 0.1f;
            }

            if (PartyModel.PartyLeader.HasVanguardPerk && isInitiator)
            {
                modifier += 0.05f;
            }

            //TODO Check if sallying out makes a party the initiator, or if the one being sallied out against is the initiator
            if (PartyModel.PartyLeader.HasVanguardPerk && isInitiator && battle.IsSallyOut)
            {
                modifier += 0.1f;
            }

            if (PartyModel.PartyLeader.HasRearguardPerk && ! isInitiator && battle.IsSiegeOutside)
            {
                modifier += 0.1f;
            }

            return modifier;
        }


        internal float GetDefendingModifierFromLeaderPerks(Troop attacker, TerrainType terrain, bool isInitiator, MapEvent battle)
        {
            if (!PartyModel.HasLeader) return 1f;

            float modifier = 1f;

            if (PartyModel.PartyLeader.HasLooseFormationsPerk && IsInfantry && attacker.IsRanged)
            {
                modifier += 0.1f;
            }

            if (PartyModel.PartyLeader.HasEliteReservesPerk && CharacterObject.Tier >= 3)
            {
                modifier += 0.2f;
            }

            return modifier;
        }



        public override string ToString()
        {
            return CharacterObject.ToString();
        }
    }
}
