using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : IState
{
    private WalkerEnemy _enemy;
    private Vector3 _retreatTarget;
    private float _retreatDistance = 3f;

    public RetreatState(WalkerEnemy enemy)
    {
        _enemy = enemy;
    }
    public void Enter()
    {
        Vector3 dir = (_enemy.transform.position - _enemy.Target.position).normalized;
        _retreatTarget = _enemy.transform.position + dir * _retreatDistance;
        _enemy.Agent.isStopped = false;
        _enemy.Agent.SetDestination(_retreatTarget);
    }

    public void Execute()
    {
        float distance = Vector3.Distance(_enemy.transform.position, _retreatTarget);

        if (distance <= 0.5f)
        {
            _enemy.ChangeState(new ChaseState(_enemy));
        }
    }

    public void Exit()
    {
        //nothing for now
    }
}
