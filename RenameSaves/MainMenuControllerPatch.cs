using HarmonyLib;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace BananaDude508.RenameSaves
{
	[HarmonyPatch(typeof(MainMenuController))]
	internal class MainMenuControllerPatch
	{
		[HarmonyPatch(nameof(MainMenuController.Start))]
		[HarmonyPostfix]
		public static void Start_Postfix(MainMenuController __instance)
		{
			// Create a Canvas if not already present
			GameObject NGObject = GameObject.Find("NewGame");

			// Create a GameObject for the Input Field
			GameObject inputFieldObject = new GameObject("TextMeshPro InputField");
			inputFieldObject.transform.SetParent(NGObject.transform);

			// Add TMP_InputField Component
			TMP_InputField inputField = inputFieldObject.AddComponent<TMP_InputField>();

			// Add RectTransform Component
			RectTransform rectTransform = inputField.GetComponent<RectTransform>();
			rectTransform.sizeDelta = new Vector2(400, 50); // Width: 400, Height: 50
			rectTransform.anchoredPosition = new Vector2(0, 0); // Center of the screen

			// Add Image Component for Background
			Image backgroundImage = inputFieldObject.AddComponent<Image>();
			backgroundImage.color = new Color(0.2f, 0.2f, 0.2f, 0.8f); // Dark gray, semi-transparent

			// Create Placeholder Text
			GameObject placeholderObject = new GameObject("Placeholder");
			placeholderObject.transform.SetParent(inputFieldObject.transform);
			TextMeshProUGUI placeholderText = placeholderObject.AddComponent<TextMeshProUGUI>();
			placeholderText.text = "Enter text...";
			placeholderText.fontSize = 18;
			placeholderText.color = new Color(0.8f, 0.8f, 0.8f, 0.8f); // Light gray
			placeholderText.alignment = TextAlignmentOptions.Center;
			RectTransform placeholderRect = placeholderText.GetComponent<RectTransform>();
			placeholderRect.sizeDelta = rectTransform.sizeDelta;
			placeholderRect.anchorMin = Vector2.zero;
			placeholderRect.anchorMax = Vector2.one;
			placeholderRect.offsetMin = Vector2.zero;
			placeholderRect.offsetMax = Vector2.zero;

			// Create Input Text
			GameObject textObject = new GameObject("Text");
			textObject.transform.SetParent(inputFieldObject.transform);
			TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
			textComponent.fontSize = 18;
			textComponent.color = Color.black;
			textComponent.alignment = TextAlignmentOptions.Center;
			RectTransform textRect = textComponent.GetComponent<RectTransform>();
			textRect.sizeDelta = rectTransform.sizeDelta;
			textRect.anchorMin = Vector2.zero;
			textRect.anchorMax = Vector2.one;
			textRect.offsetMin = Vector2.zero;
			textRect.offsetMax = Vector2.zero;

			// Assign Placeholder and Text Components to Input Field
			inputField.placeholder = placeholderText;
			inputField.textComponent = textComponent;

			// Optionally: Add OnValueChanged Listener
			inputField.onValueChanged.AddListener(value =>
			{
				Debug.Log($"Input Field Value Changed: {value}");
			});
		}
	}

}

