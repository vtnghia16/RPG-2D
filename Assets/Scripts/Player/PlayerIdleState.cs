using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    // Kế thừa và ghi đè lên playerState
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Khi jump khỏi wall nhân vật sẽ velocity(0, 0)
        player.SetZeroVelocity();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // Khi nhân vật IsWallDetected thì sẽ idle
        if (xInput == player.facingDir && player.IsWallDetected())
            return;

        // Nhân vật dịch chuyển khi tấn công
        if(xInput != 0 && !player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }
}
