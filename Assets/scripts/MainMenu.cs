using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;



public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUi;
    public GameObject levelSelectUi;
    public GameObject infoMenuUi;
    public AudioSource buttonPress;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    void Start()
    {
        //SetMusicLevel(0f);

    }
    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    public void SetSfxLevel(float sliderValue)
    {
        sfxMixer.SetFloat("SoundFx", Mathf.Log10(sliderValue) * 20);
    }
    public void OpenLevelSelect()
    {
        buttonPress.Play();
        mainMenuUi.SetActive(false);
        levelSelectUi.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        buttonPress.Play();
        levelSelectUi.SetActive(false);
        mainMenuUi.SetActive(true);
        
    }

    public void OpenInfo()
    {
        buttonPress.Play();
        mainMenuUi.SetActive(false);
        infoMenuUi.SetActive(true);
    }

    public void CloseInfo()
    {
        buttonPress.Play();
        infoMenuUi.SetActive(false);
        mainMenuUi.SetActive(true);
    }
    public void QuitGame()
    {
        buttonPress.Play();
        Application.Quit();
    }
    public void Level1()
    {
        buttonPress.Play();
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        buttonPress.Play();
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        buttonPress.Play();
        SceneManager.LoadScene(3);
    }
    public void Level4()
    {
        buttonPress.Play();
        SceneManager.LoadScene(4);
    }
   
}
