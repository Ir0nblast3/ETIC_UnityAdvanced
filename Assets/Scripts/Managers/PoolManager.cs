using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
   
    [SerializeField]private int _poolSize = 20;
    [SerializeField] private BulletBehaviour _bulletPrefab;

    public GenericPool<BulletBehaviour> BulletPool { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        BulletPool = new GenericPool<BulletBehaviour>(_bulletPrefab, _poolSize, this.transform);
    }

    public BulletBehaviour GetBullet()
    {
        BulletBehaviour bullet = BulletPool.Get();
        bullet.OnSpawn();
        return bullet;
    }

    public void ReturnBulletToPool(BulletBehaviour bullet)
    {
        BulletPool.ReturnToPool(bullet);
        bullet.OnDespawn(); // Chama a lógica de despawn (limpeza)
    }
}
