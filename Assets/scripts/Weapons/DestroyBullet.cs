using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{

    public Transform explosionPrefab;
    public float destroyTime;

    private void Start()
    {
        StartCoroutine(Destroy());
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
        if (idamageable != null)
        {
            idamageable.Damage();
        }

        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(explosionPrefab, position, rotation);
        Destroy(gameObject);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
