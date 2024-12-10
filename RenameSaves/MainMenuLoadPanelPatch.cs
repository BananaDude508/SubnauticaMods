using HarmonyLib;
using TMPro;
using System.IO;
using System;
using UnityEngine;

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
			Plugin.Log.LogInfo(savePath);

			GameData gameData = GetGameData(savePath);
			lb.saveGameTimeText.text = gameData.savename;

			Plugin.Log.LogInfo($"Read data: {gameData}\nRead savename: {gameData.savename}");

			lb.saveGameTimeText.text = $"{gameData.savename} ({lb.saveGame})";
		}

		public static GameData GetGameData(string savePath)
		{
			byte[] jsonData = File.ReadAllBytes(savePath);
			GameData gameData = null;
			GameData result;
			try
			{
				if (jsonData == null)
				{
					throw new ArgumentNullException("jsonData", "No gameinfo data");
				}
				using (StreamReader streamReader = new StreamReader(new MemoryStream(jsonData)))
				{
					gameData = JsonUtility.FromJson<GameData>(streamReader.ReadToEnd());
				}
				result = gameData;
			}
			catch (Exception ex)
			{
				Plugin.Log.LogWarning($"Exception while parsing: {new object[] { ex }}.");
			}

			return gameData;
		}

		public class GameData : SaveLoadManager.GameInfo
		{
			public string savename;
		}
	}
}
