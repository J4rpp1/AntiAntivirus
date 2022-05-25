using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyWeaponSystem : MonoBehaviour
{
    public static EnemyWeaponSystem instance;
    public bool isPistolEnemy;
    public bool isShotGunEnemy;
    public bool isArEnemy;

    Enemy enemy;
    
    [SerializeField] WeaponBase pistol = null;
    [SerializeField] WeaponBase shotgun = null;
    [SerializeField] WeaponBase assaultRifle = null;
    public bool notShooting;
    public float fireRate;

    // weapon socket helps us position our weapon and graphics
    [SerializeField] Transform _weaponSocket = null;

    
    public WeaponBase EquippedWeapon { get; private set; }

    private void Awake()
    {
        instance = this;
        notShooting = true;
        enemy = FindObjectOfType<Enemy>();
       
       
        if (isPistolEnemy)
        {
            EquipWeapon(pistol);
        }
        if (isShotGunEnemy)
        {
            EquipWeapon(shotgun);
        }
        if (isArEnemy)
        {
            EquipWeapon(assaultRifle);
        }

    }

    private void Update()
    {
    

        // press Space
        if (enemy.canSeePlayer && notShooting)
        {
            //ShootWeapon();
            StartCoroutine(Shoot());
        }

    }

    public void EquipWeapon(WeaponBase newWeapon)
    {
        if (EquippedWeapon != null)
        {
            Destroy(EquippedWeapon.gameObject);
        }

        // spawn weapon in the world and hold a reference
        EquippedWeapon = Instantiate
            (newWeapon, _weaponSocket.position, _weaponSocket.rotation);
        // make sure to include it in the player GameObject so it follows
        EquippedWeapon.transform.SetParent(_weaponSocket);
    }

   /* public void ShootWeapon()
    {
        notShooting = false;
        EquippedWeapon.Shoot();
        notShooting = true;
    }*/

    IEnumerator Shoot()
    {
        notShooting = false;
        yield return new WaitForSeconds(fireRate);
        EquippedWeapon.Shoot();
        notShooting = true;
    }
}

