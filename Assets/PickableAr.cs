using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableAr : MonoBehaviour
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
        if (weaponSystem.canPickUpAr && canDestroy && weaponSystem.destroyWep)
        {
            Debug.Log("rikki");
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
