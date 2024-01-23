using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using TMPro;
using System.Collections.Generic;

public class TrackingUserLessons : MonoBehaviour
{
    public Transform parentTransform;

    void Start()
    {
        StartCoroutine(GetUsername());
    }
    public IEnumerator GetUsername()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/TrackUserProgress.php", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                string[] data = webRequest.downloadHandler.text.Split('\n'); // Split by newline character

                // Loop through each line of data
                for (int i = 0; i < data.Length; i++)
                {
                    // Split the line into columns
                    string[] columns = data[i].Split(',');

                    // Loop through each column
                    for (int j = 0; j < columns.Length; j++)
                    {
                        // Create a new TextMeshPro object
                        var textMeshPro = new GameObject("TextMeshPro " + i + "_" + j).AddComponent<TextMeshProUGUI>();

                        // Change the color to black
                        textMeshPro.color = Color.white;
                        textMeshPro.fontSize = 32;

                        // Change the alignment to left indent
                        textMeshPro.alignment = TextAlignmentOptions.Midline;

                        // Set the text property to the value
                        textMeshPro.text = columns[j].Trim();

                        // Set the parent of the TextMeshPro object
                        textMeshPro.transform.SetParent(parentTransform, false);
                    }
                }
            }
        }
    }
}
