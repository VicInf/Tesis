using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class TrackingUserLessons : MonoBehaviour
{
    public Transform parentTransform;
    public TMPro.TMP_InputField searchField;
    private string[] data;

    void Start()
    {
        StartCoroutine(GetUsername());
        searchField.onEndEdit.AddListener(SearchData);
    }
    public void SearchData(string searchText) // This method will be called whenever the InputField value changes
    {
        // Erase all child objects of parentTransform
        foreach (Transform child in new List<Transform>(parentTransform.Cast<Transform>().ToList()))
        {
            Destroy(child.gameObject);
        }
        // Loop through each line in data
        for (int i = 0; i < data.Length; i++)
        {
            // If the line contains the search text
            if (data[i].Contains(searchText))
            {
                Debug.Log("Found " + searchText + " in data!");

                // Split the found line into parts
                string[] parts = data[i].Split(',');

                // Create a new TextMeshPro object for each part of the split line
                foreach (string part in parts)
                {
                    CreateTextMeshPro(part.Trim());
                }
            }
        }
    }

    private void CreateTextMeshPro(string text)
    {
        var textMeshPro = new GameObject("TextMeshPro " + text).AddComponent<TextMeshProUGUI>();

        // Change the color to black
        textMeshPro.color = Color.white;
        textMeshPro.fontSize = 32;

        // Change the alignment to left indent
        textMeshPro.alignment = TextAlignmentOptions.Midline;

        // Set the text property
        textMeshPro.text = text;

        // Set the parent of the TextMeshPro object
        textMeshPro.transform.SetParent(parentTransform, false);

        // Position the TextMeshPro object exactly like the table
        textMeshPro.transform.localPosition = Vector3.zero;
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
                data = webRequest.downloadHandler.text.Split('\n');
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
