﻿using System.Collections;
using UnityEngine;

public class DeathBringerAttackState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.chanceToTeleport += 5;
    }

    public override void Exit()
    {
        base.Exit();

        // Get thời gian đòn tấn công cuối
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        // khi quái vật tấn công set velocity = 0
        enemy.SetZeroVelocity();


        // Gọi trigger khi quái vật dịch chuyển (ngược lại)
        if (triggerCalled)
        {
            if (enemy.CanTeleport())
            {
                stateMachine.ChangeState(enemy.teleportState);
            }
            else
            {
                stateMachine.ChangeState(enemy.battleState);

            }
        }
    }
}