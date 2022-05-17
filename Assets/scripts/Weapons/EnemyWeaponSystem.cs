using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyWeaponSystem : MonoBehaviour
{

    [SerializeField] WeaponBase _startingWeaponPrefab = null;
   
    // weapon socket helps us position our weapon and graphics
    [SerializeField] Transform _weaponSocket = null;

    
    public WeaponBase EquippedWeapon { get; private set; }

    private void Awake()
    {

        if (_startingWeaponPrefab != null)
            EquipWeapon(_startingWeaponPrefab);
    }

    private void Update()
    {
    

        // press Space
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootWeapon();

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

    public void ShootWeapon()
    {

        EquippedWeapon.Shoot();
    }
}

