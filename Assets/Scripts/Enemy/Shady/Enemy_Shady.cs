using System.Collections;
using UnityEngine;

public class Enemy_Shady : Enemy
{
    [Header("Shady spesifics")]
    public float battleStateMoveSpeed; // Tốc độ di chuyển state tấn công

    [SerializeField] private GameObject explosivePrefab;
    [SerializeField] private float growSpeed; // Tốc độ lớn của chất nổ
    [SerializeField] private float maxSize; // Kích thước tối đa

    #region States

    public ShadyIdleState idleState { get; private set; }
    public ShadyMoveState moveState { get; private set; }
    public ShadyDeadState deadState { get; private set; }
    public ShadyStunnedState stunnedState { get; private set; }
    public ShadyBattleState battleState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new ShadyIdleState(this, stateMachine, "Idle", this);
        moveState = new ShadyMoveState(this, stateMachine, "Move", this);

        deadState = new ShadyDeadState(this, stateMachine, "Dead", this);

        stunnedState = new ShadyStunnedState(this, stateMachine, "Stunned", this);
        battleState = new ShadyBattleState(this, stateMachine, "MoveFast", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    // Quay trở lại trạng thái stunnedState 
    // Khi thực hiện cửa sổ tấn công ô vuông đỏ của quái vật
    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);

    }

    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newExplosive = Instantiate(explosivePrefab, attackCheck.position, Quaternion.identity);

        // Gán các thuộc tính đã set cho explosive
        newExplosive.GetComponent<Explosive_Controller>().SetupExplosive(stats, growSpeed, maxSize, attackCheckRadius);

        cd.enabled = false;
        rb.gravityScale = 0;
    }

    public void SelfDestroy() => Destroy(gameObject);
}
