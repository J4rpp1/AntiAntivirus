using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBase
{
    public float radius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Shoot()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, 11 );
        foreach (var hitCollider in hitColliders)
        {
            IDamageable idamageable = hitCollider.gameObject.GetComponent<IDamageable>();
            if (idamageable != null)
            {
                idamageable.Damage();
            }
        }

        //‰‰ni
        AudioSource.PlayClipAtPoint(ShootSound, ProjectileSpawnLocation.position);


        /*
        ParticleSystem burstParticle = Instantiate
            (ShootParticle, ProjectileSpawnLocation.position,
            Quaternion.identity);
        burstParticle.Play();

        */

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
