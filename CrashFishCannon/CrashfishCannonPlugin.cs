using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace BananaDude508.CrashFishCannon
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    [BepInDependency("com.snmodding.nautilus")] // marks Nautilus as a dependency for this mod
    public class CrashFishCannonPlugin : BaseUnityPlugin
    {
        private const string MyGuid = "com.bananadude508.crashfishcannon";
        private const string PluginName = "Crash Fish Cannon";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);

        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Logger.LogInfo(PluginName + " " + VersionString + " " + "loaded.");
            Log = Logger;
        }
    }
}