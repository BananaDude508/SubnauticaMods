using HarmonyLib;

namespace BananaDude508.CustomStartMenu
{
    [HarmonyPatch(typeof(MenuLogo))]
    public class MenuLogoPatcher
    {
        [HarmonyPatch(nameof(MenuLogo.Start))]
        [HarmonyPrefix]
        public static bool Start_Prefix(MenuLogo __instance)
        {
            Plugin.Log.LogInfo($"Patching MenuLogo.Start()");

            __instance.gameObject.AddComponent<MenuLogoPatch>();

            return true; // Continue
        }
    }
}