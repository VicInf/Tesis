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
    public Image playerControl;
    public Image youArrow;
    public Image teacherArrow;
    public TextMeshProUGUI youDescription;
    public TextMeshProUGUI teacherDescription;
    public TextMeshProUGUI explainControl;
    public GameObject textImage;
    public GameObject youImage;
    public GameObject teacherImage;
    void Start()
    {
        StartCoroutine(ShowAndHide(playerControl, explainControl, textImage, youArrow, teacherArrow, youDescription, teacherDescription, youImage, teacherImage, 10.0f)); // 5 seconds
    }
    
    IEnumerator ShowAndHide(Image img, TextMeshProUGUI txt, GameObject txtimg, Image you, Image teacher, TextMeshProUGUI teachertxt, TextMeshProUGUI youtxt, GameObject youImg, GameObject teacherImg, float delay)
    {
        if (DBManager.levelEN == 1 && DBManager.levelFR == 1)
        {
            txt.enabled = true;
            img.enabled = true;
            txtimg.SetActive(true);
            you.enabled = true;
            teacher.enabled = true;
            teachertxt.enabled = true;
            youtxt.enabled = true;
            youImg.SetActive(true);
            teacherImg.SetActive(true);
        }
        yield return new WaitForSeconds(delay);
        if (DBManager.levelEN == 1 && DBManager.levelFR == 1)
        {
            img.enabled = false;
            txt.enabled = false;
            txtimg.SetActive(false);
            you.enabled = false;
            teacher.enabled = false;
            teachertxt.enabled = false;
            youtxt.enabled = false;
            youImg.SetActive(false);    
            teacherImg.SetActive(false);
        }
    }
}
