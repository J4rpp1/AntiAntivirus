using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponSystem : MonoBehaviour
{
    public static WeaponSystem instance;

    public Transform playerLocation;

    public float pickupDistance = 10f;
    
    public bool equipped;
    public bool pistolEquipped;
    public bool shotgunEquipped;
    public bool arEquipped;

    public Transform player;
    public bool canPickUpPistol;
    public bool canPickUpShotgun;
    public bool canPickUpAr;
    public bool destroyWep;
    public bool canDrop;
    public int currentWepAmmocount;

    [SerializeField] WeaponBase _startingWeaponPrefab = null;
    [SerializeField] WeaponBase pistolPrefab = null;
    [SerializeField] WeaponBase shotgunPrefab = null;
    [SerializeField] WeaponBase arPrefab = null;
    // weapon socket helps us position our weapon and graphics
    [SerializeField] Transform _weaponSocket = null;

    public Vector3 gizmoPosition;
    public float radius;

    public GameObject pistolDroppable;
    public GameObject shotgunDroppable;
    public GameObject arDroppable;
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
        

        if (Input.GetKeyDown(KeyCode.Mouse1) && canPickUpPistol && !equipped)
        {
            StartCoroutine(PickupPistol());
            StartCoroutine(DropTimer());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && canPickUpShotgun && !equipped)
        {

            StartCoroutine(PickupShotgun());
            StartCoroutine(DropTimer());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && canPickUpAr && !equipped)
        {

            StartCoroutine(PickupAr());
            StartCoroutine(DropTimer());
        }
        // press Space
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootWeapon();
            Sound(new Vector3(0, 0, 0), 7);
        }
        
        if (canDrop && pistolEquipped && Input.GetKeyDown(KeyCode.Mouse1))
        {
            canDrop = false;
            pistolEquipped = false;
            equipped = false;
            DropPistol();
            EquipWeapon(_startingWeaponPrefab);

        }
        if (canDrop && arEquipped && Input.GetKeyDown(KeyCode.Mouse1))
        {
            canDrop = false;
            arEquipped = false;
            equipped = false;
            DropAr();
            EquipWeapon(_startingWeaponPrefab);

        }
        if (canDrop && shotgunEquipped && Input.GetKeyDown(KeyCode.Mouse1))
        {
            canDrop = false;
            shotgunEquipped = false;
            equipped = false;
            DropShotgun();
            EquipWeapon(_startingWeaponPrefab);

        }

    }
    IEnumerator DropTimer()
    {
        yield return new WaitForSeconds(0.1f);
        canDrop = true;
    }
    IEnumerator PickupPistol()
    {
        EquipWeapon(pistolPrefab);
        equipped = true;
        pistolEquipped = true;
        destroyWep = true;
        yield return new WaitForSeconds(0.1f);
        canPickUpPistol = false;
        destroyWep = false;
    }
    IEnumerator PickupShotgun()
    {
        EquipWeapon(shotgunPrefab);
        equipped = true;
        shotgunEquipped = true;
        destroyWep = true;
        yield return new WaitForSeconds(0.1f);
        canPickUpShotgun = false;
        destroyWep = false;
    }
    IEnumerator PickupAr()
    {
        EquipWeapon(arPrefab);
        equipped = true;
        arEquipped = true;
        destroyWep = true;
        yield return new WaitForSeconds(0.1f);
        canPickUpAr = false;
        destroyWep = false;
    }
    public void DropPistol()
    {
        GameObject P = Instantiate(pistolDroppable, _weaponSocket.position, _weaponSocket.rotation);
        P.GetComponent<Rigidbody>().AddForce(P.transform.forward * 300);
    }
    public void DropShotgun()
    {
        GameObject P = Instantiate(shotgunDroppable, _weaponSocket.position, _weaponSocket.rotation);
        P.GetComponent<Rigidbody>().AddForce(P.transform.forward * 300);
    }
    public void DropAr()
    {
        GameObject P = Instantiate(arDroppable, _weaponSocket.position, _weaponSocket.rotation);
         P.GetComponent<Rigidbody>().AddForce(P.transform.forward * 300);
    }


    // MUUTA OVERLAPSPHEREIKSI 
    // MUUTA OVERLAPSPHEREIKSI 
    // MUUTA OVERLAPSPHEREIKSI 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Pistol")
            canPickUpPistol = true;
        if (other.gameObject.tag == "Shotgun")
            canPickUpShotgun = true;
        if (other.gameObject.tag == "Ar")
            canPickUpAr = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pistol")
            canPickUpPistol = false;
        if (other.gameObject.tag == "Shotgun")
            canPickUpShotgun = false;
        if (other.gameObject.tag == "Ar")
            canPickUpAr = false;
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

