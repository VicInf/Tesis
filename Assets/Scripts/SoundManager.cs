using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;

    public AudioMixer audioMixer;
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
