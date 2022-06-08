using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Processor : MonoBehaviour, IDamageable
{
    LevelSystem levelSystem;

    void Start()
    {
        levelSystem = GameObject.FindObjectOfType<LevelSystem>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        levelSystem.processorsDestroyed = levelSystem.processorsDestroyed + 1;
        Destroy(gameObject);
    }

   
}
