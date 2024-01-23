using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{

    public void Start()
    {
        LoadLanguage();
    }

    public void SaveLanguageEN()
    {
        Lua.Run("Variable['Language'] = 'en'");
        string saveData = PersistentDataManager.GetSaveData();
        PlayerPrefs.SetString("LanguageData", saveData);
        LoadLanguage();
    }

    public void SaveLanguageFR()
    {
        Lua.Run("Variable['Language'] = 'fr'");
        string saveData = PersistentDataManager.GetSaveData();
        PlayerPrefs.SetString("LanguageData", saveData);
        LoadLanguage();
    }

    public void LoadLanguage()
    {
        if (PlayerPrefs.HasKey("LanguageData"))
        {
            string saveData = PlayerPrefs.GetString("LanguageData");
            PersistentDataManager.ApplySaveData(saveData);
            string language = Lua.Run("return Variable['Language']").AsString;
            DBManager.language = language;
            DialogueManager.SetLanguage(language);
            Debug.Log("Loaded language: " + language);
        }
        else
        {
            Debug.Log("No language setting found.");
        }
    }
}
