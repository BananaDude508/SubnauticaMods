using HarmonyLib;
using TMPro;
using UnityEngine;
using Nautilus.Utility;
using System.Reflection;

namespace BananaDude508.RenameSaves
{
    [HarmonyPatch(typeof(MainMenuController))]
    internal class MainMenuControllerPatch
    {
        public static AssetBundle saveFileRenameField { get; private set; }
        public static SaveLoadManager saveLoadManager { get; private set; }

        public static string nameInput;

        [HarmonyPatch(nameof(MainMenuController.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(MainMenuController __instance)
        {
			saveLoadManager = GameObject.Find("Systems(Clone)").GetComponent<SaveLoadManager>();

            if (saveFileRenameField == null) saveFileRenameField = AssetBundleLoadingUtils.LoadFromAssetsFolder(Assembly.GetExecutingAssembly(), "saverenamefield");
            Transform newGameHeader = GameObject.Find("RightSide").transform.GetChild(2).GetChild(1);
            GameObject renameField = GameObject.Instantiate(saveFileRenameField.LoadAsset<GameObject>("SaveRenameField"), newGameHeader);

			TMP_InputField inputField = renameField.GetComponent<TMP_InputField>();
			inputField.onValueChanged = new TMP_InputField.OnChangeEvent();
			inputField.onValueChanged.AddListener((x) => nameInput = inputField.text);

            renameField.transform.localPosition += new Vector3(27.5f, 4f, 0);
        }
    }
}

