using AdvancedAutoResolve.Configuration;
using AdvancedAutoResolve.Helpers;
using AdvancedAutoResolve.Simulation;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace AdvancedAutoResolve.HarmonyPatches.MapEventPatches
{
    [HarmonyPatch]
    internal static class AddInvolvedParty
    {

        internal static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(MapEvent), "AddInvolvedPartyInternal");
        }
        internal static void Postfix(ref MapEvent __instance, PartyBase involvedParty, BattleSideEnum side)
        {
            if(SimulationsPool.TryGetSimulationModel(__instance.Id, out var simulationModel))
            {
                simulationModel.AddTroopsFromInvolvedParty(involvedParty, side);

                if (Config.CurrentConfig.ShouldLogThis(simulationModel.IsPlayerInvolved))
                {
                    MessageHelper.DisplayText($"{involvedParty} joined {simulationModel.EventDescription}", DisplayTextStyle.Info);
                }
            }
        }
    }
}
