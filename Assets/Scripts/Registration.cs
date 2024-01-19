using PixelCrushers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public TMPro.TMP_InputField nameInputField;
    public TMPro.TMP_InputField passwordInputField;
    public TMPro.TMP_InputField emailInputField;

    public Button submitButton;
    public Button showPasswordButton;
 
    void Start()
    {
        showPasswordButton.onClick.AddListener(TogglePasswordVisibility);
    }

    public void CallRegisterUser()
    {
        StartCoroutine(Register("http://localhost/sqlconnect/DataBase.php"));
    }
    public void CallRegisterTeacher()
    {
        StartCoroutine(Register("http://localhost/sqlconnect/TeacherDataBase.php"));
    }

    IEnumerator Register(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameInputField.text); 
        form.AddField ("password", passwordInputField.text);
        form.AddField("email", emailInputField.text);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    if (webRequest.downloadHandler.text == "0")
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("RegisterMainMenu");
                    }
                    break;
            }
        }
    }
    void TogglePasswordVisibility()
    {
        if (passwordInputField.contentType == TMP_InputField.ContentType.Password)
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Password;
        }

        passwordInputField.ForceLabelUpdate();
    }

    public void VerifyInputs()
    {
       submitButton.interactable = (nameInputField.text.Length >= 4 && passwordInputField.text.Length >=8 && emailInputField.text.Length >= 8);
    }
}
