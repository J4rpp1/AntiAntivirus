using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public Transform explosionPrefab;
    [SerializeField] float _travelSpeed = 2f;
    [SerializeField] float _lifeTime = 1.5f;

    Rigidbody _rb = null;
    ProjectilePool _projectilePool = null;
    Coroutine _selfDestructRoutine = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (_selfDestructRoutine != null)
            StopCoroutine(_selfDestructRoutine);
        _selfDestructRoutine = StartCoroutine(DestroyAfterSeconds(_lifeTime));
    }

    private void OnDisable()
    {
        if (_selfDestructRoutine != null)
            StopCoroutine(_selfDestructRoutine);
    }

    public void AssignPool(ProjectilePool projectilePool)
    {
        _projectilePool = projectilePool;
    }

    private void FixedUpdate()
    {
        Travel(_rb);
    }

    protected void Travel(Rigidbody rb)
    {
        Vector3 moveOffset = transform.forward * _travelSpeed;
        rb.MovePosition(rb.position + moveOffset);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
        if(idamageable != null)
        {
            idamageable.Damage();
        }
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point;
        Instantiate(explosionPrefab, position, rotation);
        RemoveSelf();
    }

    void RemoveSelf()
    {
        if (_projectilePool != null)
        {
            
            _projectilePool.ReturnToPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterSeconds(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        RemoveSelf();
    }

}
