using BepInEx;
using BepInEx.Logging;
using HarmonyLib;


namespace BananaDude508.CustomStartMenu
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    [BepInDependency("com.snmodding.nautilus")] // marks Nautilus as a dependency for this mod
    public class Plugin : BaseUnityPlugin
    {
        private const string MyGuid = "com.bananadude508.customstartmenu";
        private const string PluginName = "Custom Start Menu";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);

        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Log = Logger;
        }
    }
}
