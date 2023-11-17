using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flyTime = .25f; // Thời gian nhân vật bay khi thực hiện blackHole
    private bool skillUsed;


    private float defaultGravity; // Trọng lực

    public PlayerBlackholeState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        // hiện gravity và nhân vật sau khi đã cloneAttack
        defaultGravity = player.rb.gravityScale;

        skillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
        player.stats.MakeInvincible(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = defaultGravity;
        player.fx.MakeTransprent(false);
        player.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        // Set tốc độ bay lên của nhân vật theo chiều Y
        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 15);

        // Set tốc độ hạ xuống của nhân vật theo chiều Y
        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -.1f);

            if (!skillUsed)
            {
                if(player.skill.blackhole.CanUseSkill())
                    skillUsed = true;
            }
        }

        // Nếu blackHole kết thúc nhân vật chuyển sang airState
        if (player.skill.blackhole.SkillCompleted())
            stateMachine.ChangeState(player.airState);
    }

}
