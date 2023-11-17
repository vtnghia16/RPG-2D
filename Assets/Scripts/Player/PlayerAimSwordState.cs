using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Nhắm kiếm vào quái vật
public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Tạo các điểm đến mục tiêu
        player.skill.sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();

        // Set tốc độ bằng 0 khi nhân vật đang nhắm mục tiêu
        player.SetZeroVelocity();

        // Khi nhắm kiếm nhân vật chuyển qua trạng thái idle
        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        // Vị trí chuột bằng điểm camera trên màn hình
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Hướng tấn công mục tiêu theo chuột
        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
            player.Flip();
        else if(player.transform.position.x < mousePosition.x && player.facingDir == -1)
            player.Flip();
    }
}
