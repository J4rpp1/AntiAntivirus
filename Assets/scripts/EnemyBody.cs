using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyBody : MonoBehaviour
{
	//	This script removes the child relationship from the enemy body and enemy, 
	//	so that the body does not inherit the enemy's rotation and is not destroyed with it

	Animator anim;
	ParentConstraint bodyLock;

	void Start()
	{
		anim = GetComponent<Animator>();
		transform.parent = transform.parent.parent; // body parent is set to enemy parent, making them siblings in hierarchy 
	}

	public void DropBody(Vector3 deathPosition)
	{
		bodyLock.constraintActive = false;
		transform.position = deathPosition;
		
	}

	
}
