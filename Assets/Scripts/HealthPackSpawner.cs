using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _healthPackPrefab;
    private GameObject _currentHealthPack;
    private float _spawnInterval = 60f;

    public float SpawnInterval { get => _spawnInterval; set => _spawnInterval = value; }

    private void Start()
    {
        SpawnHealthPack();
        StartCoroutine(GenerateHealthPackRoutine());
    }

    IEnumerator GenerateHealthPackRoutine()
    {
        while (true) 
        { 
            if (_currentHealthPack == null)
            {
                yield return new WaitForSeconds(_spawnInterval);
                SpawnHealthPack();
            }

            yield return null;
        }
    }

    private void SpawnHealthPack()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * 1.2f;
        GameObject healthPack = ObjectPools.instance.GetFromPool(_healthPackPrefab);
        _currentHealthPack = healthPack;
        if (healthPack != null)
        {
            healthPack.transform.position = spawnPosition;
            healthPack.SetActive(true);
            Debug.Log("HealthPack Spawned");
        }
        
        //Vector3 spawnPosition = transform.position + Vector3.up * 1.2f;
        //_currentHealthPack = Instantiate(_healthPackPrefab, spawnPosition, Quaternion.identity);
        //Debug.Log("HealthPack Spawned1");
    }  
}
