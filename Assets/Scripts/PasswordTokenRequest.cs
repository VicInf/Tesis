using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class PasswordTokenRequest : MonoBehaviour
{
    public TMPro.TMP_InputField emailInputField;

    public TextMeshProUGUI dataBaseText;

    public GameObject dataBaseObject;

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
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (emailInputField.text.Length >= 8);
    }
}
