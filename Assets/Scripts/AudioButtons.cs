using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    public Button AudioButton;

    [SerializeField] private AudioSource AudioTutorialFR;
    [SerializeField] private AudioSource AudioTutorialEN;
    [SerializeField] private AudioSource AudioLevel4FR;
    [SerializeField] private AudioSource AudioLevel4EN;
    [SerializeField] private AudioSource AudioLevel5FR;
    [SerializeField] private AudioSource AudioLevel5EN;
    [SerializeField] private AudioSource AudioLevel6FR;
    [SerializeField] private AudioSource AudioLevel6EN;
    [SerializeField] private AudioSource AudioLevel7FR;
    [SerializeField] private AudioSource AudioLevel7EN;
    [SerializeField] private AudioSource AudioLevel8FR;
    [SerializeField] private AudioSource AudioLevel8EN;
    [SerializeField] private AudioSource AudioLevel9FR;
    [SerializeField] private AudioSource AudioLevel9EN;
    [SerializeField] private AudioSource AudioLevel10FR;
    [SerializeField] private AudioSource AudioLevel10EN;
    public void OnButtonClick()
    {
        int currentScene = DialogueLua.GetVariable("scene").AsInt;

        if (DBManager.language == "fr" && currentScene == 0)
        {
            AudioTutorialFR.Play();
        }
        if (DBManager.language == "en" && currentScene == 0)
        {
            AudioTutorialEN.Play();
        }
        if (DBManager.language == "fr" && currentScene == 4)
        {
            AudioLevel4FR.Play();
        }
        if (DBManager.language == "en" && currentScene == 4)
        {
            AudioLevel4EN.Play();
        }
        if (DBManager.language == "fr" && currentScene == 5)
        {
            AudioLevel5FR.Play();
        }
        if (DBManager.language == "en" && currentScene == 5)
        {
            AudioLevel5EN.Play();
        }
        if (DBManager.language == "fr" && currentScene == 6)
        {
            AudioLevel6FR.Play();
        }
        if (DBManager.language == "en" && currentScene == 6)
        {
            AudioLevel6EN.Play();
        }
        if (DBManager.language == "fr" && currentScene == 7)
        {
            AudioLevel7FR.Play();
        }
        if (DBManager.language == "en" && currentScene == 7)
        {
            AudioLevel7EN.Play();
        }
        if (DBManager.language == "fr" && currentScene == 8)
        {
            AudioLevel8FR.Play();
        }
        if (DBManager.language == "en" && currentScene == 8)
        {
            AudioLevel8EN.Play();
        }
        if (DBManager.language == "fr" && currentScene == 9)
        {
            AudioLevel9FR.Play();
        }
        if (DBManager.language == "en" && currentScene == 9)
        {
            AudioLevel9EN.Play();
        }
        if (DBManager.language == "fr" && currentScene == 10)
        {
            AudioLevel10FR.Play();
        }
        if (DBManager.language == "en" && currentScene == 10)
        {
            AudioLevel10EN.Play();
        }
    }
}
