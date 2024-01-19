using System.Collections;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PasswordReset : MonoBehaviour
{
    public TMPro.TMP_InputField emailInputField;
    public TMPro.TMP_InputField tokenInputField;
    public TMPro.TMP_InputField newPasswordInputField;
    public Button submitButton;
    public Button showPasswordButton;

    void Start()
    {
        showPasswordButton.onClick.AddListener(TogglePasswordVisibility);
    }
    public void ResetPassword()
    {
        StartCoroutine(ResetPasswordCoroutine());
    }

    private IEnumerator ResetPasswordCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", emailInputField.text);
        form.AddField("token", tokenInputField.text);
        form.AddField("new_password", newPasswordInputField.text);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/ResetPassword.php", form))
        {
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
    }
    void TogglePasswordVisibility()
    {
        if (newPasswordInputField.contentType == TMP_InputField.ContentType.Password)
        {
            newPasswordInputField.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            newPasswordInputField.contentType = TMP_InputField.ContentType.Password;
        }

        newPasswordInputField.ForceLabelUpdate();
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (tokenInputField.text.Length == 6 && newPasswordInputField.text.Length >= 8 && emailInputField.text.Length >= 8);
    }

}

