using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_AudioWindow : S_BaseWindow
{
    [SerializeField] private S_GameUI gameUI;
    [SerializeField] private S_MainUI mainUI;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    public void OnBackButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        if (gameUI != null)
        {
            gameUI.closeCurrentWindow();
        }
        else if (mainUI != null)
        {
            mainUI.closeCurrentWindow();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
 
}
