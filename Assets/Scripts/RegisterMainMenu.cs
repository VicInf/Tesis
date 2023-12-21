using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterMainMenu : MonoBehaviour
{

    public Button registerButton;
    public Button loginButton;
    public Button playButton;

    public TMP_Text playerDisplay;

    private void Start()
    {
        if (DBManager.Loggedin)
        {
            playerDisplay.text = "Player: " + DBManager.username;
        }
        registerButton.interactable = !DBManager.Loggedin;
        loginButton.interactable = !DBManager.Loggedin;
        playButton.interactable= DBManager.Loggedin;
    }
}
