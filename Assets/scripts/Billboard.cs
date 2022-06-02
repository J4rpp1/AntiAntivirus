using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	public Transform lookTarget;

	void Start()
	{
		if (!lookTarget)
			lookTarget = Camera.main.transform;
	}
	void LateUpdate()
	{
		transform.LookAt(lookTarget, Vector3.up);
	}
}
