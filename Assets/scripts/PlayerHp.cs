using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour, IDamageable
{
    public static PlayerHp instance;
    MusicPlayer musicPlayer;
    WeaponSystem weaponsystem;
    public int currentHp;
    public int maxHp;
    public bool canDie;
    public AudioClip deathSound;
    public AudioClip shieldDownSound;
    public bool isDead;
    public Animator animator;
    public Collider m_Collider;
    void Start()
    {
        instance = this;
        canDie = true;
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        weaponsystem = GameObject.FindObjectOfType<WeaponSystem>();
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponsystem.shield == 1)
            canDie = false;
        if (weaponsystem.shield == 0)
            canDie = true;
        if(currentHp == 0 && !isDead)
        {
            StartCoroutine(Die());
            
        }
    }
    public void Damage()
    {
        if(canDie)
        currentHp = 0;
        if(weaponsystem.shield == 1 && !canDie)
        StartCoroutine(ShieldDown());
    }
    IEnumerator Die()
    {
        m_Collider.enabled = false;
        animator.SetTrigger("Dead");
        isDead = true;
        SFX.instance.PlayClip(deathSound, 1f);
        yield return new WaitForSeconds(1);
        musicPlayer.levelMusic.Stop();
        musicPlayer.music.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator ShieldDown()
    {
        SFX.instance.PlayClip(shieldDownSound, 1f);
        canDie = false;

        yield return new WaitForSeconds(0.4f);
        weaponsystem.shield = 0;
        
        canDie = true;
    }
}
