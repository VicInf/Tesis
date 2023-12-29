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

    private void Awake()
    {
       /* if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        } */

     /*   playerDisplay.text = "Jugador: " + DBManager.username;
        levelDisplay.text = "Nivel: " + DBManager.level;*/
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("level", DBManager.level);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/SaveData.php", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.downloadHandler.text == "0")
                {
                    Debug.Log("Guardado Exitoso");
                    DBManager.Logout();
                    UnityEngine.SceneManagement.SceneManager.LoadScene("RegisterMainMenu");
                }
            }
            Debug.Log(webRequest.downloadHandler.text);
        }
    }

    public void IncreaseScore()
    {
        DBManager.level++;
     //  levelDisplay.text = "Nivel: " + DBManager.level;
    }
}
