﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
    protected Enemy_Slime enemy;

    public SlimeAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Slime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
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


        // Gọi trigger khi quái vật tấn công (ngược lại)
        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}