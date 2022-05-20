using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyWeaponSystem : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] WeaponBase _startingWeaponPrefab = null;
    public bool notShooting;
    public float fireRate;

    // weapon socket helps us position our weapon and graphics
    [SerializeField] Transform _weaponSocket = null;

    
    public WeaponBase EquippedWeapon { get; private set; }

    private void Awake()
    {
        notShooting = true;
        enemy = FindObjectOfType<Enemy>();
        if (_startingWeaponPrefab != null)
            EquipWeapon(_startingWeaponPrefab);
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

