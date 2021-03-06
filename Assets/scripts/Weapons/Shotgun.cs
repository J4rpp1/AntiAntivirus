using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{


    public GameObject muzzleFlash;
    List<Quaternion> pellets;
    public int bulletsPerShot = 6;
    public float spreadAngle = 10f;
    public float pelletFireVel;
    public GameObject bullet;
    public int ammoCount;
    public int MaxAmmo;
    public GameObject muzzleAni;
    public Vector3 gizmoPosition;
    public float radius = 5;
    private void Awake()
    {
        pellets = new List<Quaternion>(bulletsPerShot);
        for (int i = 0; i < bulletsPerShot; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

 
    public override void Shoot()
    {
       // ammoCount = ammoCount - 1;
        Instantiate(muzzleAni, ProjectileSpawnLocation.position, ProjectileSpawnLocation.rotation);
        StartCoroutine(MuzzleFlash());
        for (int i = 0; i < bulletsPerShot; i++)
        {
           pellets[i] = Random.rotation;
            GameObject P = Instantiate(bullet, ProjectileSpawnLocation.position, ProjectileSpawnLocation.rotation);
           P.transform.rotation = Quaternion.RotateTowards(P.transform.rotation, pellets[i], spreadAngle);
           P.GetComponent<Rigidbody>().AddForce(P.transform.forward * pelletFireVel);
            //Debug.Log("Ampuu");
        }
      
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
