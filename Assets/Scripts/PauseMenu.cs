using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;

    public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PanelOn()
    {
        pauseMenu.SetActive(false);
        PixelCrushers.UIPanel.monitorSelection = true; // Allow dialogue UI to steal back input focus again.
        PixelCrushers.UIButtonKeyTrigger.monitorInput = true; // Re-enable hotkeys.
        PixelCrushers.DialogueSystem.DialogueManager.Unpause(); // Resume DS timers (e.g., sequencer commands).
        if (DialogueManager.isConversationActive)
        {
            DialogueManager.SetDialoguePanel(true);
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        PixelCrushers.UIPanel.monitorSelection = false; // Don't allow dialogue UI to steal back input focus.
        PixelCrushers.UIButtonKeyTrigger.monitorInput = false; // Disable hotkeys.
        PixelCrushers.DialogueSystem.DialogueManager.Pause(); // Stop DS timers (e.g., sequencer commands).
        if (DialogueManager.isConversationActive)
        {
            DialogueManager.SetDialoguePanel(false);
        }
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PanelOn();
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void LoadOptions()
    {
        PanelOn();
        DialogueManager.StopAllConversations();
        SceneManager.LoadScene("GameMainMenu");
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
