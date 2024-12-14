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
			string savePath = Directory.GetCurrentDirectory() + "\\SNAppData\\SavedGames\\" + lb.saveGame + "\\gameinfo.json";
			
			GameData gameData;
			GetGameData(savePath, out gameData);

			HandleNameLoad(ref gameData);

			lb.saveGameTimeText.text = gameData.savename;
			   
			lb.saveGameTimeText.text = $"{gameData.savename} ({lb.saveGame})";
			Plugin.Log.LogInfo($"Save file name: \"{gameData.savename} ({lb.saveGame})\" ({savePath}");
			
			SaveGameData(savePath, gameData);
		}
	}
}
