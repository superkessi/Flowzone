using System;
using UnityEngine;

public class S_A_AudioManager : MonoBehaviour
{
    public static S_A_AudioManager Instance;
  
    //public S_A_Sound[] musicSounds, sfxSounds;
    //public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    //{

    //    //Instance = this;
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void Start()
    //{
    //    PlayMusic("WindBlow");
    //}

    //public void PlayMusic(string name)
    //{
    //    S_A_Sound s = Array.Find(musicSounds, x  => x.name == name);

    //    if(s == null)
    //    {
    //        Debug.Log("Sound not found");
    //    }
    //    else
    //    {
    //        musicSource.clip = s.clip;
    //        musicSource.Play();
    //    }
    //}

    //public void PlaySFX(string name)
    //{
    //    S_A_Sound s = Array.Find(sfxSounds, x => x.name == name);

    //    if (s == null)
    //    {
    //        Debug.Log("Sound not found");
    //    }
    //    else
    //    {
    //        sfxSource.PlayOneShot(s.clip);
    //    }
    //}

    //public void ToggleMusic()
    //{
    //    musicSource.mute = !musicSource.mute;
    //}

    //public void ToggleSFX()
    //{
    //    sfxSource.mute = !sfxSource.mute;
    //}

    //public void MusicVolume(float volume)
    //{
    //    musicSource.volume = volume;
    //}

    //public void SFXVolume(float volume)
    //{
    //    sfxSource.volume = volume;
    //}


    //_____________________________________________________

    [Header("---------- Audio Source ----------")]
    //[SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioSource[] musicSource;
    public AudioClip[] musicClips;

    [Header("---------- Audio Clip ----------")]
    //[SerializeField] AudioClip gameStart;

    //[Header("---------- GAME ----------")]


    [Header("---------- Background Music ----------")]
    //lizzy
    public double musicDuration;
    public double goalTime;

    public int audioToggle;
    public int soundTrackClip;

    public AudioClip currentClip; //erster Clip der abgespielt wird
    private int soundTrackChange = 0;
    private bool changeSoundtrackOne = false;
    private bool changeSoundtrackTwo = false;

    //public AudioClip backgroundOne;
    //public AudioClip backgroundTwo;
    //public AudioClip backgroundThree;


    [Header("---------- Collecting Clips ----------")]
    public AudioClip CollectDodgeSkill; // ist Jump Skill!
    public AudioClip CollectSlowMotion;
    public AudioClip CollectSpeedBoost;
    //public AudioClip CollectMagnetSkill;
    public AudioClip Collectables;

    [Header("---------- Environment Clips ----------")]
    public AudioClip Rings;
    public AudioClip wind;

    [Header("---------- Character ----------")]
    //public AudioClip windBlow;
    public AudioClip crash;
    //public AudioClip Wingspawns;
    //public AudioClip headwind;
    public AudioClip touchObsticles;

    [Header("---------- Character Use Skills ----------")]
    public AudioClip UseDodgeSkill; // ist jamp skill
    public AudioClip UseSlowMotion;
    public AudioClip UseSpeedBoost;
    //public AudioClip UseMagnetSkill;

    [Header("---------- UI ----------")]
    [Header("---------- UI Clips ----------")]
    public AudioClip UIButtonClicked;
    public AudioClip UIButtonHover;

    //[Header("---------- Animation Clips ----------")]
    //public AudioClip ScrollRollOutPressed;
    //public AudioClip ScrollRollInPressed;


    private void Start()
    {
        if (musicClips != null)
        {
            PlayMusic();
        }
        if(wind != null)
        {
            PlaySFX(wind);
        }
    }


    public void ChangeSoundtrackOne()
    {
        changeSoundtrackOne = true;   
    }

    public void ChangeSoundtrackTwo()
    {
        changeSoundtrackTwo = true;
    }

    private void PlayScheduledClip()
    {
        if (changeSoundtrackOne == true)
        {
            soundTrackChange = 1;
        }
        if (changeSoundtrackTwo == true)
        {
            changeSoundtrackOne = false;
            soundTrackChange = 2;
           
        }
        
        AudioClip audioTrackClip = musicClips[soundTrackClip];

        musicSource[audioToggle].clip = audioTrackClip;
        musicSource[audioToggle].PlayScheduled(goalTime);

        double duration = (double)audioTrackClip.samples / audioTrackClip.frequency;
        goalTime = goalTime + duration;

        audioToggle = 1 - audioToggle;

        soundTrackClip = soundTrackClip < musicClips.Length - 1 ? soundTrackClip + soundTrackChange : 2;
        
    }


    public void PlayMusic()
    {
        goalTime = AudioSettings.dspTime + 0.5;


        musicSource[audioToggle].clip = currentClip;
        musicSource[audioToggle].PlayScheduled(goalTime); //scheduled hinzugef�gts

        musicDuration = (double)currentClip.samples / currentClip.frequency; // lizzy
        goalTime = goalTime + musicDuration; //lizzy
    }

    //lizzy
    private void Update()
    {
        if (AudioSettings.dspTime > goalTime - 0.01)
        {
            PlayScheduledClip();
        }
    }

    public void StopMusic(AudioClip clip)
    {
        musicSource[audioToggle].clip = clip;
        musicSource[audioToggle].Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.loop = true;
        SFXSource.clip = clip;
        SFXSource.Play();
    }

    public void StopSFX(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Stop();
    }

    public void PlaySFXOneShoot(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayGameStartOneShoot(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
