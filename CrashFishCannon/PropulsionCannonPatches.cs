using BananaDude508.CrashFishCannon;
using HarmonyLib;
using Nautilus.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BananaDude508.CrashFishCannon
{
    [HarmonyPatch(typeof(PropulsionCannon))]
    internal class PropulsionCannonPatches
    {
        [HarmonyPatch(nameof(PropulsionCannon.Filter))]
        [HarmonyPrefix]
        public static bool Filter_Prefix(PropulsionCannon __instance, InventoryItem item, ref bool __result)
        {
            if (item == null || item.item == null || __instance.transform.IsChildOf(item.item.GetComponent<Transform>()))
            {
                __result = false;
                return false;
            }
            List<TechType> cannonAmmoWhitelist = new List<TechType>()
            {
                TechType.Crash
            };

            TechType techType = item.item.GetTechType();
            if (cannonAmmoWhitelist.Contains(item.techType))
            {
                __result = true;
                return false;
            }

            return true;
        }
    }
}