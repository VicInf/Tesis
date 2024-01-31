using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MeanLevel : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ErrorText;

    public GameObject MeanObject;
    void Start()
    {
        StartCoroutine(MeanLevelCoroutine());
    }

    private IEnumerator MeanLevelCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/MeanLevels.php", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
               MeanObject.SetActive(true);
               ErrorText.text = webRequest.error;
            }
            else
            {
               MeanObject.SetActive(true);
               LevelText.text = webRequest.downloadHandler.text; 
            }
        }
    }
}
