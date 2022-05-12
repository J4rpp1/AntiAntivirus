using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : WeaponBase
{
    public float fireRate;
    public GameObject muzzleFlash;
    public override void Shoot()
    {
        // instantiating bullet
        StartCoroutine(Shooting());

        ParticleSystem burstParticle = Instantiate
            (ShootParticle, ProjectileSpawnLocation.position,
            Quaternion.identity);
        burstParticle.Play();

        //‰‰ni
        AudioSource.PlayClipAtPoint(ShootSound, ProjectileSpawnLocation.position);
    }

    private void Update()
    {
      
    }
    IEnumerator Shooting()
    {
        muzzleFlash.SetActive(true);
        Projectile newProjectile = Instantiate
            (Projectile, ProjectileSpawnLocation.position,
            ProjectileSpawnLocation.rotation);

        Debug.Log("Ampuu");

        yield return new WaitForSeconds(fireRate);
        muzzleFlash.SetActive(false);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartCoroutine(Shooting());
        }
        
        
    }
}
