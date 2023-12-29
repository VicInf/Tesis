using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Icons;

public class LanguageManager : MonoBehaviour
{

    public void Start()
    {
        LoadLanguage();  
    }

    // Call this when the player selects English
    public void SaveLanguageEN()
    {
        Lua.Run("Variable['Language'] = 'en'");
        string saveData = PersistentDataManager.GetSaveData();
        PlayerPrefs.SetString("LanguageData", saveData);
    }

    // Call this when the player selects French
    public void SaveLanguageFR()
    {
        Lua.Run("Variable['Language'] = 'fr'");
        string saveData = PersistentDataManager.GetSaveData();
        PlayerPrefs.SetString("LanguageData", saveData);
    }

    // Call this when your game starts
    public void LoadLanguage()
    {
        if (PlayerPrefs.HasKey("LanguageData"))
        {
            string saveData = PlayerPrefs.GetString("LanguageData");
            PersistentDataManager.ApplySaveData(saveData);
            string language = Lua.Run("return Variable['Language']").AsString;
            DialogueManager.SetLanguage(language);
            // Now you can apply the language to your game
            Debug.Log("Loaded language: " + language);
        }
        else
        {
            Debug.Log("No language setting found.");
        }
    }
}
