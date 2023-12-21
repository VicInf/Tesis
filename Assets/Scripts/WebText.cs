using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class WebText : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost/sqlconnect/Webtest.php");
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Received: " + request.downloadHandler.text);
                    break;
            }
        }
    }   
}
