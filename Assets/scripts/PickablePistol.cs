using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablePistol : MonoBehaviour
{
    WeaponSystem weaponSystem;
    public bool canDestroy;
    void Start()
    {
        weaponSystem = GameObject.FindObjectOfType<WeaponSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponSystem.canPickUpPistol && canDestroy && weaponSystem.destroyWep)
        {
          
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canDestroy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canDestroy = false;
        }
    }
}
