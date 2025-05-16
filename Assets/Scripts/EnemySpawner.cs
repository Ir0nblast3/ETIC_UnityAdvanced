using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private float _enemySpawnRate = 5f;

    private void Start()
    {
        InvokeRepeating("CreateEnemies", 30f, _enemySpawnRate);
    }

    private void CreateEnemies()
    {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
}
