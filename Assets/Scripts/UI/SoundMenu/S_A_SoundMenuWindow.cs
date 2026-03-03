using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_A_SoundMenuWindow : S_A_BaseWindow
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    [SerializeField]
    private S_A_PauseUIManager gameUiManager;

    EventSystem eventSystem;

    [SerializeField]
    private GameObject firstSelectedButton;

    [SerializeField]
    private GameObject secondSelectedButton;

    private void Awake()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }

    private void Start()
    {
        //if (PlayerPrefs.HasKey("musicVolume"))
        //{
        //    LoadVolume();
        //}
        //else
        //{
        //    SetMusicVolume();
        //    SetSFXVolume();
        //}

    }

    public void OnBackButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        this.Hide();
        gameUiManager.WindowDictionary["options"].Show();
        eventSystem.SetSelectedGameObject(secondSelectedButton);
    }

    public void SetMusicVolume()
    {
        float volume =  musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume" , volume);
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

    //public void ToggleMusic()
    //{
        
    //    S_A_AudioManager.Instance.ToggleMusic();
    //}

    //public void ToggleSFX()
    //{
    //    S_A_AudioManager.Instance.ToggleSFX();
    //}

    //public void MusicVolume()
    //{
    //    S_A_AudioManager.Instance.MusicVolume(musicSlider.value);
    //}

    //public void SFXVolume()
    //{
    //    //S_A_AudioManager.Instance.SFXVolume(musicSlider.value);
    //}
}
