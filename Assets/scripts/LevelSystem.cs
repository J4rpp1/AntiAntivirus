using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;
    PauseMenu pauseMenu;

    public bool planning;
    public int processorCount;
    public int processorsDestroyed;

    public GameObject planningInterface;
    public GameObject nextLevelScreen;


    void Start()
    {
        
        instance = this;
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        pauseMenu.pause = false;
        planning = true;
        planningInterface.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(processorsDestroyed == processorCount )
        {
            LevelComplete();
        }
        if(Input.GetKeyDown(KeyCode.Space) && planning)
        {
            planning = false;
            planningInterface.SetActive(false);
        }
    }

     void LevelComplete()
    {
        Cursor.visible = true; 
        pauseMenu.pause = true;
        nextLevelScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}