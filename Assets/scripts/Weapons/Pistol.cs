using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{

    public GameObject muzzleFlash;
    public Vector3 gizmoPosition;
    public float radius;
    public void Start()
    {
           
        //muzzleFlash.SetActive(false);
    }
    public override void Shoot()
    {
        StartCoroutine(MuzzleFlash());

        Sound(new Vector3(0, 0, 0), 5);
        // instantiating bullet
        Projectile newProjectile = Instantiate
            (Projectile, ProjectileSpawnLocation.position,
            ProjectileSpawnLocation.rotation);
        
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 newPosition = transform.position + gizmoPosition;
        Gizmos.DrawWireSphere(newPosition, radius);
    }
    IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        //Debug.Log("flash");
        muzzleFlash.SetActive(false);
    }
}
