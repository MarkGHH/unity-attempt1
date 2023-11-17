using UnityEngine;
using System;

//Manages the sound throughout the entire game and makes sure the right track is played at the right moment
//Catch the active scene from the GameManager, and play the correct audio based on the current scene
//Overwrite scene audio based on incoming events such as certain dialogue, quests, cutscenes
public class AudioManager : MonoBehaviour   
{
    // To call this AudioManager and play a certain sound, make an instance of the audiomanager and pass the title of the song
    // AudioManager.Instance.PlayMusic/PlaySFX("song-title");
    // Based on tutorial: https://www.youtube.com/watch?v=rdX7nhH6jdM


    public static AudioManager Instance;

    public Sound[] musicFiles, sfxFiles;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    private void Start()
    {
        PlayMusic("MainTheme");
    }

    public void PlayMusic(string name)
    {
        //if (name == "MainTheme") name = name; Incoming string is MainTheme, but to manage these centrally they can be declared here and adjusted to whatever necessary as a way to translate to the correct name
        Sound s = Array.Find(musicFiles, x => x.name == name);

        if (s == null)
        {
            Debug.Log("No audio file");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();

        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxFiles, x => x.name == name);

        if (s == null)
        {
            Debug.Log("No audio file");
        }

        else
        {
            sfxSource.clip = s.clip;
            sfxSource.PlayOneShot(sfxSource.clip);

        }
    }
}



