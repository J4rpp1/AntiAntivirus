using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Processor : MonoBehaviour, IDamageable
{
    LevelSystem levelSystem;
    public GameObject processorSprite;
    public GameObject damagedProcessorSprite;
    public AudioClip laughter;
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
            SFX.instance.PlayClip(destroySound, 1f);
            processorSprite.SetActive(false);
            damagedProcessorSprite.SetActive(true);
            levelSystem.processorsDestroyed = levelSystem.processorsDestroyed + 1;
            StartCoroutine(Laugh());
        }

    }
    IEnumerator Laugh()
    {
        yield return new WaitForSecondsRealtime(1);
        SFX.instance.PlayClip(laughter, 1f);
    }
   
}
