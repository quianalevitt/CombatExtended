﻿using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace CombatExtended.HarmonyCE
{
    [HarmonyPatch(typeof(JobGiver_ConfigurableHostilityResponse), "TryGetAttackNearbyEnemyJob")]
    internal static class HarmonyJobGiver_ConfigurableHostilityResponse
    {
        internal static void Postfix(Pawn pawn, ref Job __result)
        {
            if (__result != null && __result.def == JobDefOf.AttackStatic)
            {
                // Check for reload
                var ammoComp = pawn.equipment.Primary?.TryGetComp<CompAmmoUser>();
                if (ammoComp == null)
                    return;

                if (!ammoComp.CanBeFiredNow)
                {
                    __result = ammoComp.HasAmmo ? JobMaker.MakeJob(CE_JobDefOf.ReloadWeapon, pawn, pawn.equipment.Primary) : JobMaker.MakeJob(JobDefOf.AttackMelee, __result.targetA);
                }
            }
        }
    }
}