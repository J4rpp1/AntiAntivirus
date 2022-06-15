using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    MusicPlayer musicPlayer;
    [SerializeField] AudioClip clickSound;
    public GameObject pauseMenuUi;
    public bool pause;
    void Start()
    {
        Time.timeScale = 1;
        pause = false;
        pauseMenuUi.SetActive(false);
    }
    private void Awake()
    {
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        instance = this;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause || Input.GetKeyDown(KeyCode.P) && !pause)
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
        musicPlayer.levelMusic.Stop();
        musicPlayer.music.Play();
        //  SFX.instance.PlayClip(clickSound, 1f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}

