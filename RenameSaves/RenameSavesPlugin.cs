using BepInEx;
using BepInEx.Logging;
using HarmonyLib;


namespace BananaDude508.RenameSaves
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        private const string MyGuid = "com.bananadude508.renamesaves";
        private const string PluginName = "Rename Saves";
        private const string VersionString = "0.1.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);

        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Log = Logger;
            Log.LogInfo("RenameSavesPluginLoaded");
        }
    }
}
