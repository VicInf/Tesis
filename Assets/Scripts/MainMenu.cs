using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer masterMixer;
    private float previousVolumeLevel;
    private bool isMuted = false;
    public Button volumeButton;
    public Slider volumeSlider;
    public Sprite muteImage; 
    public Sprite unmuteImage;

    private void Start()
    {
        masterMixer.GetFloat("Volume", out previousVolumeLevel);
        if (previousVolumeLevel <= -80)
        {
            volumeButton.image.sprite = muteImage;
            isMuted = true;
        }
        else
        {
            volumeButton.image.sprite= unmuteImage;
            isMuted = false;
        }
    }
    private void Update()
    {
        float currentVolumeLevel;
        masterMixer.GetFloat("Volume", out currentVolumeLevel);
        if (currentVolumeLevel <= -40 && !isMuted)
        {
            volumeButton.image.sprite = muteImage;
            isMuted = true;
        }
        else if (currentVolumeLevel > -40 && isMuted)
        {
            volumeButton.image.sprite = unmuteImage;
            isMuted = false;
        }
    }

    public void ToggleMuteMasterMixer()
    {
        if (isMuted)
        {
            // Unmute: set the volume to the previous level and change the button's sprite to the unmute image
            masterMixer.SetFloat("Volume", 0);
            volumeButton.image.sprite = unmuteImage;
            isMuted = false;
        }
        else
        {
            // Mute: store the current volume level, set the volume to -80 dB, and change the button's sprite to the mute image
            masterMixer.GetFloat("Volume", out previousVolumeLevel);
            masterMixer.SetFloat("Volume", -80);
            volumeButton.image.sprite = muteImage;
            isMuted = true;
        }
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
   
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
