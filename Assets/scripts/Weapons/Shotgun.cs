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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 newPosition = transform.position + gizmoPosition;
        Gizmos.DrawWireSphere(newPosition, radius);
    }
    public override void Shoot()
    {
        Sound(new Vector3(0, 0, 0), 5);
        StartCoroutine(MuzzleFlash());
        for (int i = 0; i < bulletsPerShot; i++)
        {
           pellets[i] = Random.rotation;
            GameObject P = Instantiate(bullet, ProjectileSpawnLocation.position, ProjectileSpawnLocation.rotation);
           P.transform.rotation = Quaternion.RotateTowards(P.transform.rotation, pellets[i], spreadAngle);
           P.GetComponent<Rigidbody>().AddForce(P.transform.forward * pelletFireVel);
            //Debug.Log("Ampuu");
        }
      
        //‰‰ni
        AudioSource.PlayClipAtPoint(ShootSound, ProjectileSpawnLocation.position);
       
        
        // Particles
        ParticleSystem burstParticle = Instantiate
            (ShootParticle, ProjectileSpawnLocation.position,
            Quaternion.identity);
        burstParticle.Play();

    }
    void Sound(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("SoundHeard", SendMessageOptions.DontRequireReceiver);
        }
    }
    IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        //Debug.Log("flash");
        muzzleFlash.SetActive(false);
    }
}
