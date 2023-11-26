using System.Collections;
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

        // Tăng 5% mỗi lần dịch chuyển của quái vật
        enemy.chanceToTeleport += 5;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        // khi quái vật tấn công set velocity = 0
        enemy.SetZeroVelocity();

        // Gọi trigger khi quái vật tấn công (ngược lại)
        if (triggerCalled)
        {
            if (enemy.CanTeleport())
                stateMachine.ChangeState(enemy.teleportState);
            else
                stateMachine.ChangeState(enemy.battleState);
                
        }
    }
}
