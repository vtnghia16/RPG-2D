using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1f;
        // Khi nhân vật nhả ra khỏi tường thì set X + quay hướng ngược lại
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // Chuyển đổi trạng thái nhân vật sang airState khi ở WallJumpSate
        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);

        // Khi nhân vật chạm đất thì sẽ idle
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
