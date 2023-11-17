using UnityEngine;

// Kỹ năng phản đòn của người chơi
public class PlayerCounterAttackState : PlayerState
{
    private bool canCreateClone; // Tạo nhân vật clone

    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        canCreateClone = true;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();


    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        // Xử lý các điểm anim bên trong vòng tròn này 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            // Phản đòn đánh arrow của ancher 
            if (hit.GetComponent<Arrow_Controller>() != null)
            {
                hit.GetComponent<Arrow_Controller>().FlipArrow();
                SuccesfulCounterAttack();
            }

            // Làm choáng khi bị tấn công
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned()) 
                {
                        SuccesfulCounterAttack();

                        player.skill.parry.UseSkill();

                        if (canCreateClone)
                        {
                            canCreateClone = false;
                            player.skill.parry.MakeMirageOnParry(hit.transform);
                        }
                    }
                }
        }

        // Nếu phản công fail thì chuyển sang trạng thái idle
        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    private void SuccesfulCounterAttack()
    {
        stateTimer = 10; // Bất cứ giá trị lớn hơn 1
        player.anim.SetBool("SuccessfulCounterAttack", true);
    }
}
