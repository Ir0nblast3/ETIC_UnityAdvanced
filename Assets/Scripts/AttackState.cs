using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private WalkerEnemy _enemy;

    public AttackState(WalkerEnemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.Agent.isStopped = true;

        PlayerCharacter player = _enemy.Target.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.TakeDamage(_enemy.AttackDamage);
            Debug.Log("Enemy attacks!");
        }

        _enemy.ChangeState(new RetreatState(_enemy));
    }

    public void Execute()
    {
         
    }

    public void Exit()
    {
        //nothing for now
    }
}
