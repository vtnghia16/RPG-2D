using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }

        // kiểm tra trạng thái idle khi nhân vật thoát wallSlide
        if (xInput != 0 && player.facingDir != xInput)
                stateMachine.ChangeState(player.idleState);

        // Giảm tốc độ wallSlide theo chiều Y
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        // khi nhân vật chạm đất quay về trạng thái idle
        if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);

    }

}
