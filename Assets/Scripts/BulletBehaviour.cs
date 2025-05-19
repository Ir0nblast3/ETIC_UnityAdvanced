using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletBehaviour :BaseGun
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _timer;
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            ObjectPools.instance.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectPools.instance.ReturnToPool(gameObject);

        IDamageable damagable = other.GetComponent<IDamageable>();
        if (damagable != null) 
        {
            damagable.TakeDamage(GunDamage);
        }
    }

}
