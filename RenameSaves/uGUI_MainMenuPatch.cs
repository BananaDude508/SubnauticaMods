using HarmonyLib;
using UnityEngine;
using System;
using static BananaDude508.RenameSaves.ReadWriteGameData;

namespace BananaDude508.RenameSaves
{
    [HarmonyPatch(typeof(uGUI_MainMenu))]
    internal class uGUI_MainMenuPatch
    {
        // [HarmonyPatch(nameof(uGUI_MainMenu.StartNewGame))]
        // [HarmonyPostfix]
        // public static void StartNewGame_Postfix()
        // {
        //     Plugin.Log.LogInfo("Patching uGUI_MainMenu.StartNewGame()");
        //     GameData gameData;
        // 
        //     if (!GetGameData(savePath, out gameData))
        //         Plugin.Log.LogError("Issue getting game data");
        // 
        //     string name;
        //     name = MainMenuControllerPatch.nameInput.text;
        //     if (name == null)
        //     {
        //         System.Array A = System.Enum.GetValues(typeof(TechType));
        //         name = ((TechType)A.GetValue(UnityEngine.Random.Range(0, A.Length))).ToString();
        //     }
        //     gameData.savename = name;
        // 
        //     if (!SaveGameData(savePath, gameData))
        //         Plugin.Log.LogError("Issue saving game data");
        // }
    }
}
