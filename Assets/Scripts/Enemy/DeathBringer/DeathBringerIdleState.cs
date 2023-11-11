using System.Collections;
using UnityEngine;

public class DeathBringerIdleState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;

    }

    public override void Exit()
    {
        base.Exit();

        // AudioManager.instance.PlaySFX(14, enemy.transform);
    }

    public override void Update()
    {
        base.Update();

        //if (Vector2.Distance(player.transform.position, enemy.transform.position) < 7)
        //    enemy.bossFightBegun = true;


        if (Input.GetKeyDown(KeyCode.V))
            stateMachine.ChangeState(enemy.teleportState);

        //if (stateTimer < 0 && enemy.bossFightBegun)
        //    stateMachine.ChangeState(enemy.battleState);

    }
}