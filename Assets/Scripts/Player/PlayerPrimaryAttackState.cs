using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    public int comboCounter { get; private set; }

    private float lastTimeAttacked;
    private float comboWindow = 2; // Thời gian chờ của từng primaryAttack

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // AudioManager.instance.PlaySFX(2); 

        xInput = 0;  

        // Nếu thời gian mỗi counter attack > 2 chuyển về đòn đánh counter 0
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        // Thực hiện primaryAttack 1/2/3 = ComboCounter 1/2/3
        player.anim.SetInteger("ComboCounter", comboCounter);

        // Thay đổi hướng tấn công của nhân vật (-1, 1)
        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f);

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        // Khi cpunterAttack thì tốc độ di chuyển = 0
        if (stateTimer < 0)
            player.SetZeroVelocity();

        // Set anim bằng idle của mỗi đòn đánh
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    
}
