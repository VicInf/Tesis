using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public TMPro.TMP_InputField nameInputField;
    public TMPro.TMP_InputField passwordInputField;

    public Button submitButton;
    public Button showPasswordButton;

    void Start()
    {
        showPasswordButton.onClick.AddListener(TogglePasswordVisibility);
    }
    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameInputField.text);
        form.AddField("password", passwordInputField.text);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/LogIn.php", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                // Process the response
                if (webRequest.downloadHandler.text.Split('\t')[0] == "0")
                {
                    DBManager.username = nameInputField.text;
                    DBManager.level = int.Parse(webRequest.downloadHandler.text.Split('\t')[1]);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("RegisterMainMenu");
                }
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
        submitButton.interactable = (nameInputField.text.Length >= 4 && passwordInputField.text.Length >= 8);
    }

}

