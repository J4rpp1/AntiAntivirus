using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Processor : MonoBehaviour, IDamageable
{
    LevelSystem levelSystem;
    public GameObject processorSprite;
    public GameObject damagedProcessorSprite;
    bool destroyed;
    public AudioClip destroySound;
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
        if(!destroyed)
        {
            destroyed = true;
            AudioSource.PlayClipAtPoint(destroySound, transform.position);
            processorSprite.SetActive(false);
            damagedProcessorSprite.SetActive(true);
            levelSystem.processorsDestroyed = levelSystem.processorsDestroyed + 1;

        }

    }

   
}
