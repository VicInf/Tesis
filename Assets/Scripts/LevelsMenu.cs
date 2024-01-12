using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    public GameObject menu; 
    public Button[] buttons;

    private void Start()
    {
        int level = DBManager.level;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < level; i++)
        {
            buttons[i].interactable = true;
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
