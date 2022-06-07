using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class WeaponSystem : MonoBehaviour
{
    public static WeaponSystem instance;

 
    public int maxPickupColliders = 1;
    Collider[] pickupColliders;
    public float pickupRadius = 0.5f;
    [SerializeField] LayerMask pickupMask;
    public float pickupDistance = 10f;

    public GameObject pistolImage;
    public GameObject shotgunImage;
    public GameObject arImage;

    public bool equipped;
    public bool pistolEquipped;
    public bool shotgunEquipped;
    public bool arEquipped;
    public bool knifeEquipped;
    
    public bool canPickUpPistol;
    public bool canPickUpShotgun;
    public bool canPickUpAr;
    public bool destroyWep;
    public bool canDrop;
    public int currentWepAmmocount;
    public TMP_Text ammoText;

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
        knifeEquipped = true;
        pickupColliders = new Collider[maxPickupColliders];
        instance = this;

        if (_startingWeaponPrefab != null)
            EquipWeapon(_startingWeaponPrefab);
    }

    private void Update()
    {


        /*if (Input.GetKeyDown(KeyCode.Mouse1) && canPickUpPistol && !equipped)
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
        }*/


        if (knifeEquipped)
        {
            currentWepAmmocount = 1;
            ammoText.text = "";
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentWepAmmocount > 0)
        {
            ShootWeapon();
            if(!knifeEquipped)
            Sound(new Vector3(0, 0, 0), 7);
        }
        
        if (pistolEquipped)
            ammoText.text = currentWepAmmocount.ToString() + "/10";
        if(shotgunEquipped)
            ammoText.text = currentWepAmmocount.ToString() + "/6";
        if(arEquipped)
            ammoText.text = currentWepAmmocount.ToString() + "/30";
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        TryPickupWeapon();
    }
    void TryPickupWeapon()
    {
        
        
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, pickupRadius, pickupColliders, pickupMask);
            DropWeapon();
        if(pickupColliders[0] != null)
        {
            var GetAmmo = pickupColliders[0].GetComponent<Pickable>();
            currentWepAmmocount = GetAmmo.currentAmmo;
            if (pickupColliders[0].gameObject.tag == "Pistol")
                StartCoroutine(PickupPistol());
            else if (pickupColliders[0].gameObject.tag == "Shotgun")
                StartCoroutine(PickupShotgun());
            else if (pickupColliders[0].gameObject.tag == "Ar")
                StartCoroutine(PickupAr());
            Debug.Log(pickupColliders[0].gameObject);
            Destroy(pickupColliders[0].gameObject);
            pickupColliders[0] = null;

        }
       
           
        
    }
    void DropWeapon()
    {
        if(canDrop)
        {
        if (pistolEquipped)
            DropPistol();
        else if (shotgunEquipped)
            DropShotgun();
        else if (arEquipped)
            DropAr();
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
        pistolImage.SetActive(true);
        knifeEquipped = false;
        equipped = true;
        pistolEquipped = true;
        destroyWep = true;
        yield return new WaitForSeconds(0.1f);
        canPickUpPistol = false;
        destroyWep = false;
        canDrop = true;
    }
    IEnumerator PickupShotgun()
    {
        EquipWeapon(shotgunPrefab);
        shotgunImage.SetActive(true);
        knifeEquipped = false;
        equipped = true;
        shotgunEquipped = true;
        destroyWep = true;
        yield return new WaitForSeconds(0.1f);
        canPickUpShotgun = false;
        destroyWep = false;
        canDrop = true;
    }
    IEnumerator PickupAr()
    {
        EquipWeapon(arPrefab);
        arImage.SetActive(true);
        knifeEquipped = false;
        equipped = true;
        arEquipped = true;
        destroyWep = true;
        yield return new WaitForSeconds(0.1f);
        canPickUpAr = false;
        destroyWep = false;
        canDrop = true;
    }
    public void DropPistol()
    {
        pistolImage.SetActive(false);
        knifeEquipped = true;
        canDrop = false;
        pistolEquipped = false;
        GameObject P = Instantiate(pistolDroppable, _weaponSocket.position, _weaponSocket.rotation);
        P.GetComponent<Rigidbody>().AddForce(P.transform.forward * 300);
        var GunScript = P.GetComponent<Pickable>();
        GunScript.currentAmmo = currentWepAmmocount;
    }
    public void DropShotgun()
    {
        shotgunImage.SetActive(false);
        knifeEquipped = true;
        canDrop = false;
        shotgunEquipped = false;
        GameObject P = Instantiate(shotgunDroppable, _weaponSocket.position, _weaponSocket.rotation);
        P.GetComponent<Rigidbody>().AddForce(P.transform.forward * 300);
        var GunScript = P.GetComponent<Pickable>();
        GunScript.currentAmmo = currentWepAmmocount;
    }
    public void DropAr()
    {
        arImage.SetActive(false);
        knifeEquipped = true;
        canDrop = false;
        arEquipped = false;
        GameObject P = Instantiate(arDroppable, _weaponSocket.position, _weaponSocket.rotation);
         P.GetComponent<Rigidbody>().AddForce(P.transform.forward * 300);
        var GunScript = P.GetComponent<Pickable>();
        GunScript.currentAmmo = currentWepAmmocount;
    }


 
    
   /* private void OnTriggerEnter(Collider other)
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
    }*/
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
        if(!knifeEquipped  && !arEquipped)
            currentWepAmmocount = currentWepAmmocount - 1;
        EquippedWeapon.Shoot();
    }
}

