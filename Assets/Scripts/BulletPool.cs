using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private Queue<GameObject> coinPool = new Queue<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        CreateBulletsForPool();
        CreateCoinForPool();
    }

    private void CreateBulletsForPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    private void CreateCoinForPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(false);
            coinPool.Enqueue(coin);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(true);
            return bullet;
        }
    }

    public GameObject GetCoin()
    {
        if (coinPool.Count > 0)
        {
            GameObject coin = coinPool.Dequeue();
            coin.SetActive(true);
            return coin;
        }
        else
        {
            GameObject coin = Instantiate(bulletPrefab);
            coin.SetActive(true);
            return coin;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        coinPool.Enqueue(coin);
    }
}
