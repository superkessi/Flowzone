using UnityEngine;

public class S_A_VolumeSettings : MonoBehaviour
{

    //[SerializeField] S_A_SoundMenuWindow soundMenuWindow;
    [SerializeField] S_AudioWindow audioWindow;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            //soundMenuWindow.LoadVolume();
            audioWindow.LoadVolume();
        }
        else
        {
            //soundMenuWindow.SetMusicVolume();
            //soundMenuWindow.SetSFXVolume();
            audioWindow.SetSFXVolume();
            audioWindow.SetMusicVolume();
        }
    }

    
}
