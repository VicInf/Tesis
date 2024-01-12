using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public void SceneLoaded(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
