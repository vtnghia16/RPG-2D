using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        // Kỹ năng blackHole của nhân vật
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (player.skill.blackhole.cooldownTimer > 0)
            {
                player.fx.CreatePopUpText("Cooldown!");
                return;
            }

            stateMachine.ChangeState(player.blackHole);
        }

        // Kỹ năng nhắm kiếm của nhân vật
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
            stateMachine.ChangeState(player.aimSowrd);

        // Nhấn Q chuyển sàn trạng thái phản công của nhân vật
        if (Input.GetKeyDown(KeyCode.Q)) 
            stateMachine.ChangeState(player.counterAttack);

        // Tấn công theo combo
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
    }

    // Check ĐK khi nhân vật không có kiếm thì sẽ ReturnSword
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
