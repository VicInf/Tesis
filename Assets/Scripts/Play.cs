using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public void LevelLoaded(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
