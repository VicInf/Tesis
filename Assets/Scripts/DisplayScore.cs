using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

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
        // Get the value of the last scene
        int lastScenePlayed = DialogueLua.GetVariable("scene").AsInt;

        int questionsAsked = DialogueLua.GetVariable("questionsAsked").AsInt;

        if (DBManager.language == "en")
        {
            int currentHighScore = PlayerPrefs.GetInt(DBManager.username + "/levelEN" + lastScenePlayed + "/highscoreEN");
            int totalQuestions = PlayerPrefs.GetInt(DBManager.username + "/levelEN" + lastScenePlayed + "/totalQuestionsAskedEN");
            if (PlayerPrefs.GetInt(DBManager.username + "/levelEN" + lastScenePlayed + "/playedBefore", 0) == 0)
            {
                // If not, update the totalQuestions variable
                totalQuestions += questionsAsked;
                int totalQuestionsAskedEN = PlayerPrefs.GetInt(DBManager.username + "/totalQuestionsAskedEN");
                totalQuestionsAskedEN += totalQuestions;
                PlayerPrefs.SetInt(DBManager.username + "/totalQuestionsAskedEN", totalQuestionsAskedEN);

                // Set the flag to indicate that the level has been played before
                PlayerPrefs.SetInt(DBManager.username + "/levelEN" + lastScenePlayed + "/playedBefore", 1);
            }
            LevelHighestScore(score, currentHighScore, lastScenePlayed);
        }
        if (DBManager.language == "fr")
        {
            int currentHighScore = PlayerPrefs.GetInt(DBManager.username + "/levelFR" + lastScenePlayed + "/highscoreFR");
            int totalQuestions = PlayerPrefs.GetInt(DBManager.username + "/levelFR" + lastScenePlayed + "/totalQuestionsAskedFR");
            if (PlayerPrefs.GetInt(DBManager.username + "/levelFR" + lastScenePlayed + "/playedBefore", 0) == 0)
            {
                // If not, update the totalQuestions variable
                totalQuestions += questionsAsked;
                int totalQuestionsAskedFR = PlayerPrefs.GetInt(DBManager.username + "/totalQuestionsAskedFR");
                totalQuestionsAskedFR += totalQuestions;
                PlayerPrefs.SetInt(DBManager.username + "/totalQuestionsAskedFR", totalQuestionsAskedFR);

                // Set the flag to indicate that the level has been played before
                PlayerPrefs.SetInt(DBManager.username + "/levelFR" + lastScenePlayed + "/playedBefore", 1);
            }
            LevelHighestScore(score, currentHighScore, lastScenePlayed);
        }

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
            motivation.text = "¡Excelente trabajo!\n Has avanzado de nivel.";
            StartCoroutine(UpdateLevelDataEN(DBManager.username, lastScenePlayed));
            winningSoundEffect.Play();
            if (lastScenePlayed >= DBManager.levelEN)
            {
                DBManager.levelEN++;
            }
            reachEnd = true;
        }
        if (!reachEnd && score >= 5 && DBManager.language == "fr")
        {
            motivation.text = "¡Excelente trabajo!\n Has avanzado de nivel.";
            StartCoroutine(UpdateLevelDataFR(DBManager.username, lastScenePlayed));
            winningSoundEffect.Play();
            if (lastScenePlayed >= DBManager.levelFR)
            {
                DBManager.levelFR++;
            }
            reachEnd = true;
        }
    }

    private void LevelHighestScore(int score, int currentHighScore, int lastScenePlayed)
    {
        if (score > currentHighScore)
        {
            // Calculate the difference between the new score and the current high score
            int scoreDifference = score - currentHighScore;

            // Update the high score for the levelEN
            if (DBManager.language == "en")
            {
                int totalCorrectAnswersEN = PlayerPrefs.GetInt(DBManager.username + "/totalcorrectanswersEN");
                totalCorrectAnswersEN += scoreDifference;
                PlayerPrefs.SetInt(DBManager.username + "/totalcorrectanswersEN", totalCorrectAnswersEN);
                int totalQuestionsAskedEN = PlayerPrefs.GetInt(DBManager.username + "/totalQuestionsAskedEN");
                PlayerPrefs.SetInt(DBManager.username + "/levelEN" + lastScenePlayed + "/highscoreEN", score);
                string scoreMessageEN = "Respuestas Correctas: " + totalCorrectAnswersEN + "/" + totalQuestionsAskedEN;
                PlayerPrefs.SetString(DBManager.username + "/scoreMessageEN", scoreMessageEN);
            }
            // Update the high score for the levelFR
            if (DBManager.language == "fr")
            {
                int totalCorrectAnswersFR = PlayerPrefs.GetInt(DBManager.username + "/totalcorrectanswersFR");
                totalCorrectAnswersFR += scoreDifference;
                PlayerPrefs.SetInt(DBManager.username + "/totalcorrectanswersFR", totalCorrectAnswersFR);
                int totalQuestionsAskedFR = PlayerPrefs.GetInt(DBManager.username + "/totalQuestionsAskedFR");
                PlayerPrefs.SetInt(DBManager.username + "/levelFR" + lastScenePlayed + "/highscoreFR", score);
                string scoreMessageFR = "Respuestas Correctas: " + totalCorrectAnswersFR + "/" + totalQuestionsAskedFR;
                PlayerPrefs.SetString(DBManager.username + "/scoreMessageFR", scoreMessageFR);
            }     
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
            }
        }
    }
}
