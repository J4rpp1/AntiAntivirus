using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IDamageable
{
    WeaponSystem weaponsystem;
    public int currentHp;
    public int maxHp;
    public bool canDie;
    void Start()
    {
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
            Destroy(gameObject);
        }
    }
    public void Damage()
    {
        if(canDie)
        currentHp = 0;
        if(weaponsystem.shield == 1 && !canDie)
        StartCoroutine(ShieldDown());
    }
    IEnumerator ShieldDown()
    {
        canDie = false;
        yield return new WaitForSeconds(0.2f);
        weaponsystem.shield = 0;
        
        canDie = true;
    }
}
