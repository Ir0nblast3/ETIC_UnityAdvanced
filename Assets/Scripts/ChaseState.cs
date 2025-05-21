using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private WalkerEnemy _enemy;

    public ChaseState(WalkerEnemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.Agent.isStopped = false;
        Debug.Log("Entered CHASE");
    }

    public void Execute()
    {
        if (_enemy.Target != null)
        {
            _enemy.Agent.SetDestination(_enemy.Target.position);
        }
    }

    public void Exit()
    {
        //nothing for now
    }
}
