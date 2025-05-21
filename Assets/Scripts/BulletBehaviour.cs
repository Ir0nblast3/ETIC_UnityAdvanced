using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletBehaviour :BaseGun
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _timer;
    private bool _canStart = false;

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
       
        if (_canStart == true)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                ObjectPools.instance.ReturnToPool(gameObject);
                _canStart = false;
            }
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

    public void BulletTimer()
    {
        _timer = 2f;
        _canStart = true;
    }

}
