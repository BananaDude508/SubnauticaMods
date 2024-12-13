using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BananaDude508.RenameSaves
{
    public class ReadWriteGameData
    {
        public static string savePath;

        public static bool GetGameData(string savePath, out GameData result) // Returns bool for error catching
        {
            byte[] jsonData = File.ReadAllBytes(savePath);
            GameData gameData = null;
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
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"Exception while loading: {new object[] { ex }}.");
                result = null;
                return false;
            }
        }

        public static bool SaveGameData(string savePath, GameData gameData) // Returns bool for error catching
        {
            if (gameData == null) return false;

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(savePath))
                {
                    streamWriter.Write(gameData.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"Exception while saving: {new object[] { ex }}.");
                return false;
            }
        }

        public class GameData : SaveLoadManager.GameInfo
        {
            public string savename;
        }
    }
}
