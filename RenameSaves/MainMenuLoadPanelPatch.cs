using HarmonyLib;
using System.IO;
using UnityEngine;
using static BananaDude508.RenameSaves.ReadWriteGameData;


namespace BananaDude508.RenameSaves
{
    [HarmonyPatch(typeof(MainMenuLoadPanel))]
    internal class MainMenuLoadPanelPatch
    {
        [HarmonyPatch(nameof(MainMenuLoadPanel.UpdateLoadButtonState))]
        [HarmonyPostfix]
        public static void UpdateLoadButtonState_Postfix(MainMenuLoadPanel __instance, MainMenuLoadButton lb)
        {
            savePath = Directory.GetCurrentDirectory() + "\\SNAppData\\SavedGames\\" + lb.saveGame + "\\gameinfo.json";

            GameData gameData;
            GetGameData(savePath, out gameData);

            string name;
            name = gameData.savename;
            if (name == null)
            {
                System.Array A = System.Enum.GetValues(typeof(TechType));
                name = ((TechType)A.GetValue(UnityEngine.Random.Range(0, A.Length))).ToString();
            }

            lb.saveGameTimeText.text = name;

            lb.saveGameTimeText.text = $"{gameData.savename} ({lb.saveGame})";
            Plugin.Log.LogInfo($"Save file name: \"{gameData.savename} ({lb.saveGame})\"");
        }
    }
}
