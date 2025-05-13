using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour :BaseGun, IPoolable
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private float _timer;

    public void OnDespawn()
    {
        
    }

    public void OnSpawn()
    {
        _timer = _lifeTime;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            PoolManager.Instance.ReturnBulletToPool(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PoolManager.Instance.ReturnBulletToPool(this);

        IDamageable damagable = other.GetComponent<IDamageable>();
        if (damagable != null) 
        {
            damagable.TakeDamage(GunDamage);
        }
    }
}
