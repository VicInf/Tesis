using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class PasswordTokenRequest : MonoBehaviour
{
    public TMPro.TMP_InputField emailInputField;
    public Button submitButton;
    public void RequestPasswordResetToken()
    {
        StartCoroutine(RequestTokenCoroutine());
    }

    private IEnumerator RequestTokenCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", emailInputField.text);
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/ResetToken.php", form))
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

    public void VerifyInputs()
    {
        submitButton.interactable = (emailInputField.text.Length >= 8);
    }
}
