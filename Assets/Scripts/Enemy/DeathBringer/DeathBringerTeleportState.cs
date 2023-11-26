using System.Collections;
using UnityEngine;

// Trạng thái dịch chuyển của quái vật
public class DeathBringerTeleportState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerTeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // Ẩn quái vật khi teleport
        enemy.stats.MakeInvincible(true);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            // Kiểm tra quái vật sử dụng kỹ năng phép 
            if (enemy.CanDoSpellCast())
                stateMachine.ChangeState(enemy.spellCastState);
            else
                stateMachine.ChangeState(enemy.battleState);
        } 
    }

    public override void Exit()
    {
        base.Exit();

        // Hiện quái vật khi teleport kết thúc
        enemy.stats.MakeInvincible(false);
    }
}
