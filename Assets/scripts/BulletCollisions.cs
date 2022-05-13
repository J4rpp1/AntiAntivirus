using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour
{
	ParticleSystem cannonParticles;
	List<ParticleCollisionEvent> cannonHitEvents;
	public Transform hitParticle;
	private Vector3 hitPosition;
	private Vector3 hitNormal;
	void Start()
	{
		cannonParticles = GetComponent<ParticleSystem>();
		cannonHitEvents = new List<ParticleCollisionEvent>();
	}

	// Update is called once per frame
	void OnParticleCollision(GameObject other)
	{
		//Populate magical list of particle collision events to get intersection and normal
		ParticlePhysicsExtensions.GetCollisionEvents(cannonParticles, other, cannonHitEvents);
		hitPosition = cannonHitEvents[0].intersection;
		hitNormal = cannonHitEvents[0].normal;
		//Spawn hit particles at precise position of hit, facing away from hit surface
		Instantiate(hitParticle, hitPosition, Quaternion.LookRotation(hitNormal, Vector3.up));
		//Using an Interface, apply damage to target and pass hitPosition so the target can use it in death sequence
		//other.GetComponent<IDamageable<Vector3>>().Damage(hitPosition);
	}
}
