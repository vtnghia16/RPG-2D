﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player       = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        // Set trạng thái cho nhân vật
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        // Lấy gốc tọa độ nhân vật theo chiều x
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.anim.SetFloat("yVelocity", rb.velocity.y);

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }
}
