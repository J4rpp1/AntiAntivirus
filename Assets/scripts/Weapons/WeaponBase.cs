using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    public abstract void Shoot();

    [SerializeField] Projectile _projectile = null;
    protected Projectile Projectile => _projectile;

    [SerializeField] Transform _projectileSpawnLocation = null;
    protected Transform ProjectileSpawnLocation => _projectileSpawnLocation;

    [SerializeField] ParticleSystem _shootParticle = null;
    protected ParticleSystem ShootParticle => _shootParticle;

    [SerializeField] AudioClip _shootSound = null;
    protected AudioClip ShootSound => _shootSound;

 
}
