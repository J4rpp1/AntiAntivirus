using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBase
{
	public float radius = 0.5f;
	[SerializeField] LayerMask enemyLayer;
	[SerializeField] Animator knifeAnim;
	bool canSlash = true;
/*     void Start()
	{
		
	} */
	public override void Shoot()
	{

		if(canSlash)
			Slash();


		/*
		ParticleSystem burstParticle = Instantiate
			(ShootParticle, ProjectileSpawnLocation.position,
			Quaternion.identity);
		burstParticle.Play();

		*/

	}
	// Update is called once per frame
	void Slash()
	{
		canSlash = false;
		knifeAnim.SetTrigger("Slash");

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
	}

	void Slashed() //animation event when slash completed
	{
		canSlash = true;
	}
}
