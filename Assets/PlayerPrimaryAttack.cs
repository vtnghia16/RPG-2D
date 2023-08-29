using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;

    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Set combo tấn công của nhân vật
        if(comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);

        // Set tốc độ khi nhân vật vừa di chuyển và tấn công
        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, 
                           player.attackMovement[comboCounter].y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        // Kiểm tra xem nhân vật đang đứng yên hay hoạt động
        player.StartCoroutine("BusyFor", .15f);

        comboCounter++;
        lastTimeAttacked = Time.time;

    }

    public override void Update()
    {
        base.Update();

        // Điều khiển nhân vật dừng lại khi tấn công
        if(stateTimer < 0 )
        {
            player.ZeroVelocity();
        }

        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
