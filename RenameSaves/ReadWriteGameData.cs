using System;
using System.IO;
using UnityEngine;
using static BananaDude508.RenameSaves.ReadWriteGameData;

namespace BananaDude508.RenameSaves
{
    public class ReadWriteGameData
    {
        public static bool GetGameData(string savePath, out GameData gameData) // Returns bool for error catching
        {
            byte[] jsonData = File.ReadAllBytes(savePath);
            try
            {
                if (jsonData == null)
                    throw new ArgumentNullException("jsonData", "No gameinfo data");
                using (StreamReader streamReader = new StreamReader(new MemoryStream(jsonData)))
                    gameData = JsonUtility.FromJson<GameData>(streamReader.ReadToEnd());
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"Exception while loading: {new object[] { ex }}.");
                gameData = null;
                return false;
            }
        }

        public static bool SaveGameData(string savePath, GameData gameData) // Returns bool for error catching
        {
            if (gameData == null) return false;

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(savePath))
                    streamWriter.Write(gameData.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"Exception while saving: {new object[] { ex }}.");
                return false;
            }
        }
		public static string HandleNameLoad(ref GameData gameData)
        {
			string text = MainMenuControllerPatch.nameInput;
            Plugin.Log.LogInfo($"{gameData.savename} or {text}");
			if (gameData.savename == null || gameData.savename.Replace(" ", "") == "")
				if (text != null && text.Replace(" ", "") != "")
				{
					gameData.savename = text;
					MainMenuControllerPatch.nameInput = "";
				}
				else
					gameData.savename = GetRandomSaveName();
            return gameData.savename;
		}

        private static string[] randomSaveNames =
        {
            "Sea Treader",
            "Reaper Leviathan",
            "Reefback",
            "Ghost Leviathan",
            "Sea Dragon",
            "Sea Emperor",
            "Sand Shark",
            "Bone Shark",
            "Peeper",
            "Oculus",
            "Cave Crawler",
            "Scanner",
            "Lifepod",
            "Aurora",
            "Degasi",
            "New World"
        };
        public static string GetRandomSaveName()
        {
            return randomSaveNames[UnityEngine.Random.Range(0, randomSaveNames.Length+1)];
		}

		public class GameData : SaveLoadManager.GameInfo
        {
            public string savename;
        }
    }
}
