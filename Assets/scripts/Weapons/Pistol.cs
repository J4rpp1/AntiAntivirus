using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    
    public GameObject muzzleFlash;
    public GameObject muzzleAnimation;
    
 
    public void Start()
    {
        
        //muzzleFlash.SetActive(false);
    }
    public override void Shoot()
    {
       
        StartCoroutine(MuzzleFlash());

        Instantiate(muzzleAnimation, ProjectileSpawnLocation.position, ProjectileSpawnLocation.rotation);
        // instantiating bullet
        Projectile newProjectile = Instantiate
            (Projectile, ProjectileSpawnLocation.position,
            ProjectileSpawnLocation.rotation);
        
        //��ni
        SFX.instance.PlayClip(ShootSound, 1f);
       

        // Particles
        ParticleSystem burstParticle = Instantiate
            (ShootParticle, ProjectileSpawnLocation.position,
            Quaternion.identity);
        burstParticle.Play();



    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        //Debug.Log("flash");
        muzzleFlash.SetActive(false);
    }
}
