using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlip : MonoBehaviour
{
	SpriteRenderer weaponSprite;
	void Start()
	{
		weaponSprite = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if(Vector3.Dot(transform.right, Vector3.right) > 0f)
		{
			weaponSprite.flipY = true;
		}
		else
		{
			weaponSprite.flipY = false;
		}
	}
}
