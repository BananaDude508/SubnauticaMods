using HarmonyLib;
using UnityEngine;
using System;
using static BananaDude508.RenameSaves.ReadWriteGameData;
using static BananaDude508.RenameSaves.MainMenuControllerPatch;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace BananaDude508.RenameSaves
{
	[HarmonyPatch(typeof(IngameMenu))]
	internal class IngameMenuPatch
	{
		private static string savePath;
		private static GameData gameData;

		[HarmonyPatch(nameof(IngameMenu.SaveGameAsync))]
		[HarmonyPostfix]
		public static IEnumerator SaveGameAsync_Postfix(IEnumerator original)
		{
			bool prefixError = false;
			string savePath = string.Empty;

			prefixError = SGA_Pre();
			while (original.MoveNext())
				yield return original.Current;
			SGA_Post(prefixError);
		}

		private static bool SGA_Pre()
		{
			try
			{
				savePath = Directory.GetCurrentDirectory() + "\\SNAppData\\SavedGames\\" + saveLoadManager.GetCurrentSlot() + "\\gameinfo.json";
				if (!GetGameData(savePath, out gameData))
					throw new Exception("Failed to get game data");
			}
			catch (Exception ex)
			{
				Plugin.Log.LogError(ex);
				return true;
			}
			return false;
		}

		private static void SGA_Post(bool prefixError)
		{
			if (prefixError)
				savePath = Directory.GetCurrentDirectory() + "\\SNAppData\\SavedGames\\" + saveLoadManager.GetCurrentSlot() + "\\gameinfo.json";

			HandleNameLoad(ref gameData);

			SaveGameData(savePath, gameData);
			Plugin.Log.LogInfo(JsonUtility.ToJson(gameData, true));
		}
	}
}


