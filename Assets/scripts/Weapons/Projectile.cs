using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{

        [SerializeField] float _travelSpeed = 5f;
    [SerializeField] float _lifeTime = 1.5f;

    Rigidbody _rb = null;
    
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

  

    private void FixedUpdate()
    {
        Travel(_rb);
    }

    protected void Travel(Rigidbody rb)
    {
        Vector3 moveOffset = transform.forward * _travelSpeed;
        rb.MovePosition(rb.position + moveOffset);
    }

    private void OnCollisionEnter(Collision other)
    {
        RemoveSelf();
    }

    void RemoveSelf()
    {
      
            Destroy(gameObject);
        
    }

    IEnumerator DestroyAfterSeconds(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        RemoveSelf();
    }

}
