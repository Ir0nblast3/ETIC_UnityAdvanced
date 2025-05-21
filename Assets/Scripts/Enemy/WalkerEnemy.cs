using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkerEnemy : MonoBehaviour, IDamageable
{
    private IState _currentState;

    private NavMeshAgent _agent;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _coinPrefab;

    private float _hp = 100;
    private float _attackDamage = 10;

    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Transform Target { get => _target; set => _target = value; }
    public float AttackDamage { get => _attackDamage; set => _attackDamage = value; }

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

        ChangeState(new ChaseState(this));
    }

    void Update()
    {
        //_agent.SetDestination(_target.position);
        _currentState?.Execute();
    }

    public void ChangeState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0) 
        {
            Agent.isStopped = true;
            ChangeState(null);

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

        if (player != null && _currentState is ChaseState) 
        {
            //player.TakeDamage(_attackDamage);
            ChangeState(new AttackState(this));
        }
    }
}
