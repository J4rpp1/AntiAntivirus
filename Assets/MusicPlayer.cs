using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static GameObject instance;
    public AudioSource music;
    public AudioSource levelMusic;

    void Start()

    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
        PlayMenuMusic();
    }

    void PlayMenuMusic()
    {
        music.Play();

    }

    void Update()
    {
    }
}
