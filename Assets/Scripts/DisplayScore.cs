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

    void Update()
    {
        // Get the value of the 'score' variable
        int score = DialogueLua.GetVariable("score").AsInt;
        int lastScenePlayed = DialogueLua.GetVariable("scene").AsInt;

        // Update the UI Text element with the value of 'score'
        scoreText.text = "Respuestas correctas: " + score.ToString();

        if(score <= 1 )
        {
            motivation.text = "¡Puedes hacerlo mejor!";
        }
        if(score <= 4 ) 
        {
            motivation.text = "¡Bien hecho!";
        }
        if(!reachEnd && score >= 5 ) 
        {
            motivation.text = "¡Excelente trabajo!";
            StartCoroutine(UpdateLevelData(DBManager.username, lastScenePlayed));
            reachEnd = true;
        }
        if(score == 0 )
        {
            motivation.text = "Debes esforzarte mas, ¡Puedes hacerlo!";
        }
    }

    IEnumerator UpdateLevelData(string playerToken, int lastScenePlayed)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerToken", playerToken);
        form.AddField("lastScenePlayed", lastScenePlayed);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/UpdateLevelButton.php", form))
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
                    DBManager.level = int.Parse(webRequest.downloadHandler.text.Split('\t')[1]);
                    Debug.Log(DBManager.level);
                }
            }
        }
    }
}


