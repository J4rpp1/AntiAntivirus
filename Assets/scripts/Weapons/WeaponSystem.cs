using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponSystem : MonoBehaviour
{
    public static WeaponSystem instance;

    public Transform playerLocation;

    [SerializeField] WeaponBase _startingWeaponPrefab = null;
    [SerializeField] WeaponBase _slot01WeaponPrefab = null;
    [SerializeField] WeaponBase _slot02WeaponPrefab = null;
    // weapon socket helps us position our weapon and graphics
    [SerializeField] Transform _weaponSocket = null;

    public Vector3 gizmoPosition;
    public float radius;
    

    // our weapon will use the STRATEGY PATTERN
    // each new weapon will have its own behavior!
    public WeaponBase EquippedWeapon { get; private set; }

    private void Awake()
    {
        instance = this;

        if (_startingWeaponPrefab != null)
            EquipWeapon(_startingWeaponPrefab);
    }

    private void Update()
    {
        // press 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(_slot01WeaponPrefab);
        }

        // press 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(_slot02WeaponPrefab);
        }

        // press Space
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootWeapon();
            Sound(new Vector3(0, 0, 0), 5);
        }
      
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 newPosition = transform.position + gizmoPosition;
        Gizmos.DrawWireSphere(newPosition, radius);
    }
    void Sound(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("SoundHeard", SendMessageOptions.DontRequireReceiver);
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

