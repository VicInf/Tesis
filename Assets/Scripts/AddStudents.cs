using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Net;

public class AddStudents : MonoBehaviour
{
    public TMPro.TMP_InputField usernameInputField;

    public Button submitButton;

    public void AddtoClass()
    {
        StartCoroutine(AddStudent());
    }

    IEnumerator AddStudent()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameInputField.text);
        form.AddField("teacherUsername", DBManager.username);

        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/AddStudent.php", form);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (usernameInputField.text.Length >= 4);
    }
}