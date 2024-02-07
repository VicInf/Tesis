using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelsMenu : MonoBehaviour
{
    public GameObject menu; 
    public Button[] buttons;
    public TMP_Text showScore;

    private void Start()
    {
        if (DBManager.language == "fr")
        {
            int levelFR = DBManager.levelFR;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }

            for (int i = 0; i < levelFR; i++)
            {
                buttons[i].interactable = true;
            }
            string score = PlayerPrefs.GetString(DBManager.username + "/scoreMessageFR", "");
            showScore.text = score;
        }
        if (DBManager.language == "en")
        {
            int levelEN = DBManager.levelEN;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }

            for (int i = 0; i < levelEN; i++)
            {
                buttons[i].interactable = true;
            }
            string score = PlayerPrefs.GetString(DBManager.username + "/scoreMessageEN", "");
            showScore.text = score;
        }
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }
}
