using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBase
{
    public float radius = 0.5f;
    [SerializeField] LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Shoot()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            IDamageable idamageable = hitCollider.gameObject.GetComponent<IDamageable>();
            if (idamageable != null)
            {
                idamageable.Damage();
            }
        }

        //��ni
        SFX.instance.PlayClip(ShootSound, 1f);


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
