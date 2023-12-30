using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bắt kiếm vào quái vật
public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Check if player or player.sword is null before accessing properties
        if (player == null || player.sword == null)
        {
            return;
        }

        sword = player.sword.transform;

        player.fx.PlayDustFX();
        player.fx.ScreenShake(player.fx.shakeSwordImpact); // Hiện tượng rung lắc sword

        // Bắt thanh kiếm cùng hướng hướng với thanh kiếm khi quay về
        if (player.transform.position.x > sword.position.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < sword.position.x && player.facingDir == -1)
            player.Flip();

        // Tốc độ nhân vật khi thanh kiếm trả về 
        rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);
    }


    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .1f);
    }

    public override void Update()
    {
        base.Update();

        // Thanh kiếm được quay về nhân vật chuyển sang idle
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

}
