using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    public AudioSource music;
    public AudioSource levelMusic;
    public AudioSource buttonPress;
    public AudioSource winSound;
    void Start()

    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        music.Play();

    }

    public void PlayLevelMusic()
    {
        levelMusic.Play();
    }
    void Update()
    {
    }
}
