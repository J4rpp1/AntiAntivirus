using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by: Adam Chandler, using tutorials from Unity3D.college
/// This script allows you to create your own object pool scripts from this template.
/// To use:
/// 1. Inherit from this class to create any type of Object pool you want.
/// 2. Make sure to clean up your objects before sending them back to the pool. This script
/// 3. Optionally, override ResetDefaults() to do extra work on object before returning to pool
/// does not return object default settings: that is left to the user.
/// </summary>
public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
    [Header("Pool Settings")]
    [SerializeField] private T _prefab = null;
    [SerializeField] private int _startingPoolSize = 10;

    protected Queue<T> _objectPool = new Queue<T>();

    
    private void Awake()
    {
        CheckReferences();
        CreateInitialPool(_startingPoolSize);
    }
    

 
    public T ActivateFromPool()
    {
       
        if (_objectPool.Count == 0)
        {
            CreateNewPoolObject();
        }

        T newPoolObject = _objectPool.Dequeue();

       
        return newPoolObject;
    }

    public void ReturnToPool(T objectToReturn)
    {
        ResetObjectDefaults(objectToReturn);
        // disable just in case
        objectToReturn.gameObject.SetActive(false);
        _objectPool.Enqueue(objectToReturn);
    }
   

    protected virtual void ResetObjectDefaults(T pooledObject)
    {

    }

    private void CheckReferences()
    {
        if (_prefab == null)
        {
            Debug.LogError(this + ": no pool prefab defined");
            this.enabled = false;
            return;
        }
    }

    private void CreateInitialPool(int startingPoolSize)
    {
        for (int i = 0; i < startingPoolSize; i++)
        {
            CreateNewPoolObject();
        }
    }

    private void CreateNewPoolObject()
    {
        T newObject = Instantiate(_prefab);

        newObject.transform.SetParent(this.transform);
        newObject.gameObject.name = _prefab.gameObject.name;
        newObject.gameObject.SetActive(false);
        Debug.Log("Enqueue");
        _objectPool.Enqueue(newObject);
    }
}
