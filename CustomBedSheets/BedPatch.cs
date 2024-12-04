using HarmonyLib;
using JetBrains.Annotations;
using Nautilus.Utility;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BananaDude508.CustomBedSheets
{
    [HarmonyPatch(typeof(Bed))]
    internal class BedPatch
    {
        [HarmonyPatch(nameof(Bed.Start))]
        [HarmonyPrefix]
        public static bool Start_Prefix(Bed __instance)
        {
            Transform mattress;
            string targetImage = "default.png";
            Transform firstChild =__instance.transform.GetChild(0);
            if (__instance.transform.childCount == 2) //...........// Double bed
                if (firstChild.GetChild(0).childCount == 3) //.....// Normal double bed
                {
                    targetImage = "DoubleBasic.png";
                    mattress = firstChild.GetChild(0).GetChild(0);
                }
                else //............................................// Quilted double bed
                {
                    targetImage = "DoubleQuilt.png";
                    mattress = firstChild.GetChild(0).GetChild(1);
                }
            else //................................................// Single bed
            {
                targetImage = "Single.png";
                mattress = firstChild.GetChild(2);
            }

            Texture2D texture = ImageUtils.LoadTextureFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", targetImage));
            
            if (texture == null)
            {
                CustomBedsheetsPlugin.Log.LogInfo($"Assets/{targetImage} does not exist, default texture used");
                return true;
            }

            MeshRenderer mattressRenderer = mattress.GetComponent<MeshRenderer>();
            Material mattressMaterial = mattressRenderer.material;
            mattressMaterial.SetTexture("_MainTex", texture);
            mattressMaterial.mainTexture.wrapMode = TextureWrapMode.Repeat;
            mattressMaterial.mainTextureScale = new Vector2(1.8f, 1.41f);
            mattressMaterial.mainTextureOffset = new Vector2(0.29f, -0.07f);
            mattressRenderer.material = mattressMaterial;

            return true;
        }
    }
}
