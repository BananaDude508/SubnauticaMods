using HarmonyLib;
using UnityEditor;

namespace BananaDude508.CustomStartMenu
{
    [HarmonyPatch(typeof(PerformanceConsoleCommands))]
    internal class PerformanceConsoleCommandsPatch
    {
        public static PerformanceConsoleCommands performanceConsoleCommands;
        
        [HarmonyPatch(nameof(PerformanceConsoleCommands.Awake))]
        [HarmonyPrefix]
        public static void Awake_Postfix(PerformanceConsoleCommands __instance)
        {
            performanceConsoleCommands = __instance;
            bool srPrefabExists = performanceConsoleCommands.sizeRefPrefab != null;
			Plugin.Log.LogInfo($"PerformanceConsoleCommands.sizerefPrefab exists: {srPrefabExists}");
            if (!srPrefabExists) return;
        }
    }
}
