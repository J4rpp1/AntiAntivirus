using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour, IDamageable
{
    public static PlayerHp instance;
    WeaponSystem weaponsystem;
    public int currentHp;
    public int maxHp;
    public bool canDie;
    public AudioClip deathSound;
    public AudioClip shieldDownSound;
    public bool isDead;
    void Start()
    {
        instance = this;
        canDie = true;
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
        if(currentHp == 0)
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
        isDead = true;
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator ShieldDown()
    {
        AudioSource.PlayClipAtPoint(shieldDownSound, transform.position);
        canDie = false;

        yield return new WaitForSeconds(0.4f);
        weaponsystem.shield = 0;
        
        canDie = true;
    }
}
