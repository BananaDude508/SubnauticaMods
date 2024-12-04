using BepInEx;
using BepInEx.Logging;
using HarmonyLib;


namespace BananaDude508.CustomBedSheets
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    [BepInDependency("com.snmodding.nautilus")] // marks Nautilus as a dependency for this mod
    public class CustomBedsheetsPlugin : BaseUnityPlugin
    {
        private const string MyGuid = "com.bananadude508.custombedsheets";
        private const string PluginName = "Custom Bedsheets";
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
