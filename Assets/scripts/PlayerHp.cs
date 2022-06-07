using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IDamageable
{

    public int currentHp;
    public int maxHp;

    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp == 0)
        {
            Destroy(gameObject);
        }
    }
    public void Damage()
    {
        currentHp = 0;
    }
}
