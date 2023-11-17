using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{


    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.dash.CloneOnDash(); // Tạo ra nhiều bản sao cho nhân vật
        stateTimer = player.dashDuration;

        player.stats.MakeInvincible(true);

    }

    public override void Exit()
    {
        base.Exit();

        player.skill.dash.CloneOnArrival();

        // Thoát khỏi trạng thái dash x = 0
        player.SetVelocity(0, rb.velocity.y);

        player.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);

        // Set tốc độ dash của nhân vật
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        // Hết thời gian state chuyển qua trạng thái idle
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

        player.fx.CreateAfterImage();
    }
}
