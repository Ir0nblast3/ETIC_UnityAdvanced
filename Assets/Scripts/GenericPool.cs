using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool<T> where T : MonoBehaviour, IPoolable
{
    //Test to see if GitHub is working correctly
    private Queue<T> pool = new Queue<T>();
    private T _prefab;
    private Transform _location;

    public GenericPool(T _prefab, int poolSize, Transform _location = null)
    {
        this._prefab = _prefab;
        this._location = _location;

        for (int i = 0; i < poolSize; i++) 
        { 
            T obj = GameObject.Instantiate(_prefab, _location);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        T obj = pool.Count > 0  ? pool.Dequeue() : GameObject.Instantiate(_prefab, _location);
        obj.gameObject.SetActive(true);
        obj.OnSpawn();
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.OnDespawn();
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
