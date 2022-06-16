using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSorting : MonoBehaviour
{
	SpriteRenderer weaponSprite;
	void Start()
	{
		weaponSprite = GetComponentInChildren<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector3.Dot(transform.forward, Vector3.forward) > 0f)
		{
			weaponSprite.sortingOrder = -1;
		}
		else
		{
			weaponSprite.sortingOrder = 1;
		}
	}
}
