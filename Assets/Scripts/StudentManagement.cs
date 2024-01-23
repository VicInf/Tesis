using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Net;

public class StudentManagement: MonoBehaviour
{
    public TMPro.TMP_InputField usernameInputField;

    public TextMeshProUGUI dataBaseText;

    public GameObject dataBaseObject;

    public Button submitButton;

    public void AddtoClass()
    {
        StartCoroutine(StudentManager("http://localhost/sqlconnect/AddStudent.php"));
    }

    public void KickFromClass()
    {
        StartCoroutine(StudentManager("http://localhost/sqlconnect/KickStudent.php"));
    }

    IEnumerator StudentManager(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameInputField.text);
        form.AddField("teacherUsername", DBManager.username);
        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            dataBaseObject.SetActive(true);
            dataBaseText.text = webRequest.error;
        }
        else
        {
            if (webRequest.downloadHandler.text.Split('\t')[0] != "0")
            {
                dataBaseObject.SetActive(true);
                dataBaseText.text = webRequest.downloadHandler.text;
            }
            else
            {
                dataBaseObject.SetActive(true);
                dataBaseText.text = webRequest.downloadHandler.text.Split('\t')[1];
            }
        }
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (usernameInputField.text.Length >= 4);
    }
}
