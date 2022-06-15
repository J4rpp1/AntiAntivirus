using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : WeaponBase
{
    WeaponSystem weaponSystem;
    public float fireRate;
    public GameObject muzzleFlash;
    public Vector3 gizmoPosition;
    public float radius = 5;
    bool isShooting;

    private void Start()
    {
        weaponSystem = GameObject.FindObjectOfType<WeaponSystem>();
    }
    public override void Shoot()
    {
        if(!isShooting)
        StartCoroutine(Shooting());

        //ääni
    }

    private void Update()
    {
       
    }
    
    IEnumerator Shooting()
    {
        isShooting = true;
        weaponSystem.currentWepAmmocount = weaponSystem.currentWepAmmocount - 1;
        muzzleFlash.SetActive(true);
        Projectile newProjectile = Instantiate
            (Projectile, ProjectileSpawnLocation.position,
            ProjectileSpawnLocation.rotation);


        ParticleSystem burstParticle = Instantiate
            (ShootParticle, ProjectileSpawnLocation.position,
            Quaternion.identity);
        burstParticle.Play();
        //Debug.Log("Ampuu");

        yield return new WaitForSeconds(fireRate);
        muzzleFlash.SetActive(false);
        isShooting = false;
        AudioSource.PlayClipAtPoint(ShootSound, ProjectileSpawnLocation.position);
        if (Input.GetKey(KeyCode.Mouse0) && weaponSystem.currentWepAmmocount > 0)
        {
            StartCoroutine(Shooting());
        }
        
    }
  
}
