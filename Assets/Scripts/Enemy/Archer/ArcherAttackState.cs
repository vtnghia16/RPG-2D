using System.Collections;
using UnityEngine;

public class ArcherAttackState : EnemyState
{
    private Enemy_Archer enemy;
    public ArcherAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Archer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
