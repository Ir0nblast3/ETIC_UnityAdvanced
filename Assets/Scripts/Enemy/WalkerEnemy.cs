using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkerEnemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent _agent;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _coinPrefab;

    private float _hp = 100;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) 
        { 
            _target = player.transform;
        }
    }

    void Update()
    {
        _agent.SetDestination(_target.position);
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0) 
        {
            ObjectPools.instance.ReturnToPool(gameObject);
            Debug.Log("Enemy died");
            
            GameObject coin = ObjectPools.instance.GetFromPool(_coinPrefab);

            if (coin != null) 
            {
                coin.transform.position = transform.position;
                coin.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayerCharacter player = other.gameObject.GetComponent<PlayerCharacter>();

        if (player != null) 
        {
            player.TakeDamage(10);
        }
    }
}
