using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;
    PauseMenu pauseMenu;
    MusicPlayer musicPlayer;
    public bool planning;
    public int processorCount;
    public int processorsDestroyed;
    private Scene scene;

    public GameObject planningInterface;
    public GameObject nextLevelScreen;
    public GameObject nextLevelButton;
    bool levelIsCompleted;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        instance = this;
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        pauseMenu.pause = false;
        planning = true;
        planningInterface.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (processorsDestroyed == processorCount && !levelIsCompleted )
        {
            LevelComplete();
        }
        if(Input.GetKeyDown(KeyCode.Space) && planning)
        {
            musicPlayer.music.Pause();
            musicPlayer.levelMusic.Play(); 
            planning = false;
            planningInterface.SetActive(false);
        }
    }

     void LevelComplete()
    {
        levelIsCompleted = true;
        musicPlayer.levelMusic.Stop();
        musicPlayer.winSound.Play();
        Cursor.visible = true; 
        pauseMenu.pause = true;
        nextLevelScreen.SetActive(true);
        if (scene.buildIndex == 3)
            nextLevelButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void NextLevelButton()
    {
        musicPlayer.buttonPress.Play();
        musicPlayer.levelMusic.Stop();
        musicPlayer.music.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MainMenuButton()
    {
        musicPlayer.buttonPress.Play();
       
        musicPlayer.music.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
