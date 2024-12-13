using HarmonyLib;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using UnityEngine.Assertions;
using Nautilus.Utility;
using System.Reflection;

namespace BananaDude508.RenameSaves
{
    [HarmonyPatch(typeof(MainMenuController))]
    internal class MainMenuControllerPatch
    {
        public static AssetBundle SaveFileRenameField { get; private set; }
        public static TMP_InputField nameInput;

        [HarmonyPatch(nameof(MainMenuController.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(MainMenuController __instance)
        {
            if (SaveFileRenameField == null) SaveFileRenameField = AssetBundleLoadingUtils.LoadFromAssetsFolder(Assembly.GetExecutingAssembly(), "saverenamefield");
            GameObject renameField = SaveFileRenameField.LoadAsset<GameObject>("SaveRenameField");
            Transform newGameHeader = GameObject.Find("RightSide").transform.GetChild(2).GetChild(1);

            Plugin.Log.LogInfo($"SaveRenameField exists: {renameField!=null}");
            Plugin.Log.LogInfo($"NewGame exists: {newGameHeader != null} ({newGameHeader.name})");
            Plugin.Log.LogInfo($"Read name: {nameInput.text}");

            renameField = GameObject.Instantiate(renameField, newGameHeader);
            renameField.transform.localPosition += new Vector3(27.5f, 4f, 0);

            nameInput = renameField.GetComponent<TMP_InputField>();
        }
    }
}

