using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    /* public TMP_Text playerDisplay;
     public TMP_Text levelDisplay;*/

    public Image playerControl;
    public TextMeshProUGUI explainControl;
    public GameObject textImage;
    void Start()
    {
        StartCoroutine(ShowAndHide(playerControl, explainControl, textImage, 5.0f)); // 5 seconds
    }
    private void Awake()
    {
        /* if (DBManager.username == null)
         {
             UnityEngine.SceneManagement.SceneManager.LoadScene(0);
         } */

        /*   playerDisplay.text = "Jugador: " + DBManager.username;
           levelDisplay.text = "Nivel: " + DBManager.level;*/
    }
    IEnumerator ShowAndHide(Image img, TextMeshProUGUI txt, GameObject txtimg, float delay)
    {
        txt.enabled = true;
        img.enabled = true;
        txtimg.SetActive(true);
        yield return new WaitForSeconds(delay);
        img.enabled = false;
        txt.enabled = false;
        txtimg.SetActive(false);
    }
}
