using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private float _enemySpawnRate;

    private void Start()
    {
        InvokeRepeating("CreateEnemies", 45f, _enemySpawnRate);
    }

    private void CreateEnemies()
    {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
}
