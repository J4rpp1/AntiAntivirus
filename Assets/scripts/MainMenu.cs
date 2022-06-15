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

    public AudioMixer musicMixer;
    void Start()
    {
        SetMusicLevel(0f);

    }
    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    public void OpenLevelSelect()
    {
        mainMenuUi.SetActive(false);
        levelSelectUi.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        levelSelectUi.SetActive(false);
        mainMenuUi.SetActive(true);
        
    }

    public void OpenInfo()
    {
        mainMenuUi.SetActive(false);
        infoMenuUi.SetActive(true);
    }

    public void CloseInfo()
    {
        infoMenuUi.SetActive(false);
        mainMenuUi.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        SceneManager.LoadScene(3);
    }
    public void Level4()
    {
        SceneManager.LoadScene(4);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
