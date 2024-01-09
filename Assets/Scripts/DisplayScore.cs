using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    // Reference to your UI Text element
    public TMP_Text scoreText;
    public TMP_Text motivation;

    void Update()
    {
        // Get the value of the 'score' variable
        int score = DialogueLua.GetVariable("score").AsInt;

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
        if(score >= 5 ) 
        {
            motivation.text = "¡Excelente trabajo!";
        }
        if(score == 0 )
        {
            motivation.text = "Debes esforzarte mas, ¡Puedes hacerlo!";
        }
    }
}
