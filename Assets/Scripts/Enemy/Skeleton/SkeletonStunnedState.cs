using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quái vật bị choáng khi tấn công
public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // Hiệu ứng lặp lại nhấp nháy đỏ & độ trễ, độ lặp
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);

        stateTimer = enemy.stunDuration;

        // Tốc độ bị choáng khi dính đòn
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        // Quái vật sẽ chuyển trạng thái idle khi bị dính choáng
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
