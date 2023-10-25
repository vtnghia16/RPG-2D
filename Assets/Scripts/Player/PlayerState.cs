using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    // Lấy gốc tọa độ của nhân vật
    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer; // Bộ đếm timer theo delta
    protected bool triggerCalled; // kết hợp anim khi gọi

    // Hàm xây dựng
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName= _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true); // Set tham số animation cho nhân vật
        rb = player.rb;
        triggerCalled = false;

        // Debug.Log("stateTimer enter: " + stateTimer);

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        // Gốc tọa độ x, y theo nhân vật
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);


        // Debug.Log("stateTimer update: " + stateTimer);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    // Trigger gọi anim với bất kì trạng thái nào
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    
}
