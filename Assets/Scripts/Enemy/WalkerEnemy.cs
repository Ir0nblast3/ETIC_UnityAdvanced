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

    void Update()
    {
        _agent.SetDestination(_target.position);
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0) 
        {
            Destroy(gameObject);
            Debug.Log("Enemy died");
            Instantiate(_coinPrefab, transform.position, Quaternion.Euler(0, 0, 90));
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
