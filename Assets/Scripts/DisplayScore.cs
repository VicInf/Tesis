using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class DisplayScore : MonoBehaviour
{
    // Reference to your UI Text element
    public TMP_Text scoreText;
    public TMP_Text motivation;
    private bool reachEnd = false;

    [SerializeField] private AudioSource winningSoundEffect;
    [SerializeField] private AudioSource lossingSoundEffect;
    [SerializeField] private AudioSource goodSoundEffect;

    void Awake()
    {
        // Get the value of the 'score' variable
        int score = DialogueLua.GetVariable("score").AsInt;
        int lastScenePlayed = DialogueLua.GetVariable("scene").AsInt;

        // Update the UI Text element with the value of 'score'
        scoreText.text = "Respuestas correctas: " + score.ToString();

        if (score == 0)
        {
            motivation.text = "Debes esforzarte más, ¡Puedes hacerlo!";
            motivation.color = Color.white;
            lossingSoundEffect.Play();
        }
        if (score == 1 )
        {
            motivation.text = "¡Puedes hacerlo mejor!";
            motivation.color = Color.white;
            lossingSoundEffect.Play();
        }
        if(score <= 4 && score >1) 
        {
            motivation.text = "¡Bien hecho!";
            motivation.color = Color.white;
            goodSoundEffect.Play();
        }
        if(!reachEnd && score >= 5 && DBManager.language == "en") 
        {
            motivation.text = "¡Excelente trabajo!";
            StartCoroutine(UpdateLevelDataEN(DBManager.username, lastScenePlayed));
            winningSoundEffect.Play();
            reachEnd = true;
        }
        if (!reachEnd && score >= 5 && DBManager.language == "fr")
        {
            motivation.text = "¡Excelente trabajo!";
            StartCoroutine(UpdateLevelDataFR(DBManager.username, lastScenePlayed));
            winningSoundEffect.Play();
            reachEnd = true;
        }
    }

    IEnumerator UpdateLevelDataFR(string playerToken, int lastScenePlayed)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerToken", playerToken);
        form.AddField("lastScenePlayed", lastScenePlayed);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/UpdateLevelButtonFR.php", form))
        {
            yield return webRequest.SendWebRequest();
  
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("Level data update complete!");
                if (webRequest.downloadHandler.text.Split('\t')[0] == "0")
                {
                    DBManager.levelFR = int.Parse(webRequest.downloadHandler.text.Split('\t')[1]);
                    Debug.Log(DBManager.levelFR);
                }
            }
        }
    }
    IEnumerator UpdateLevelDataEN(string playerToken, int lastScenePlayed)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerToken", playerToken);
        form.AddField("lastScenePlayed", lastScenePlayed);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/UpdateLevelButtonEN.php", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("Level data update complete!");
                if (webRequest.downloadHandler.text.Split('\t')[0] == "0")
                {
                    DBManager.levelEN = int.Parse(webRequest.downloadHandler.text.Split('\t')[1]);
                    Debug.Log(DBManager.levelEN);
                }
            }
        }
    }
}


