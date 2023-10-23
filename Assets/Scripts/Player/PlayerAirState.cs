using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // Kiểm tra bề mặt tiếp xúc giữa wall và nhân vật
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);

        // == 0 thì nhân vật chạm đất
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        // Nhân vật khi trượt tường
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }
}
