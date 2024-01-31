using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    public Button AudioButton;

    [SerializeField] private AudioSource AudioLevel4FR;
    [SerializeField] private AudioSource AudioLevel4EN;
    [SerializeField] private AudioSource AudioLevel5FR;
    [SerializeField] private AudioSource AudioLevel5EN;
    public void OnButtonClick()
    {
        int currentScene = DialogueLua.GetVariable("scene").AsInt;

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
    }
}
