using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanguage : MonoBehaviour
{

    public void Language(string language)
    {
        DialogueManager.SetLanguage(language);
    }
}
