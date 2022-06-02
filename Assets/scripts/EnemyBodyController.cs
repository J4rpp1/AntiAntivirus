using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyController : MonoBehaviour
{
	[SerializeField] GameObject body;
	EnemyBody enemyBody;
	void Start() 
	{
		enemyBody = body.GetComponent<EnemyBody>();
	}
    void OnDestroy()
    {
        enemyBody.DropBody(transform.position);
    }
}
