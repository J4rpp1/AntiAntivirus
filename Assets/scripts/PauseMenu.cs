using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    public GameObject pauseMenuUi;
    public static PauseMenu instance;
    public bool pause;
    void Start()
    {

        pause = false;
        pauseMenuUi.SetActive(false);
    }
    private void Awake()
    {
        instance = this;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) && !pause)
        {
            Cursor.visible = true;
            pauseMenuUi.SetActive(true);
            pause = true;

            Time.timeScale = 0;
        }


    }

    public void Resume()
    {
       // SFX.instance.PlayClip(clickSound, 1f);
        pauseMenuUi.SetActive(false);
        pause = false;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        //  SFX.instance.PlayClip(clickSound, 1f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}

