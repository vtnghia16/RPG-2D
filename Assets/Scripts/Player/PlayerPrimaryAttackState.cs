using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    public int comboCounter { get; private set; } // Kết hợp primary 1/2/3

    private float lastTimeAttacked; // Thời gian tấn công cuối cùng của mỗi đòn đánh
    private float comboWindow = 2; // Giới hạn thực hiện mỗi đòn đánh trong combo

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //AudioManager.instance.PlaySFX(2); // attack sound effect

        xInput = 0;  // fix bug của attack direction

        // Khi thực hiện 3 đòn đánh thì return 0
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        // Debug.Log(comboCounter);

        player.anim.SetInteger("ComboCounter", comboCounter); // Thực hiện primaryAttack 1/2/3 = ComboCounter 1/2/3

        // Thay đổi hướng tấn công của nhân vật
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;

        // Nhân vật di chuyển (x, y) set theo từng comboCounter
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        //player.anim.speed = 3;

        stateTimer = .1f; 
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f); // Set nhân vật idle 0.15s khi attack

        // Thực hiện đòn đánh tiếp theo và gán delta time với lastTimeAttacked
        comboCounter++; 
        lastTimeAttacked = Time.time;


        //player.anim.speed = 1;

        // Debug.Log(lastTimeAttacked);
    }

    public override void Update()
    {
        base.Update();

        // Khi nhân vật attack => velocity(0, 0)
        if (stateTimer < 0)
            player.SetZeroVelocity();

        // khi nhân vật tấn công thì trạng thái idle ngưng hoạt động (ngược lại)
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

}
