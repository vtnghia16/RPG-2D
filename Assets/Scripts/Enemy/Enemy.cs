using System.Collections;
using UnityEngine;

// Lấy chung các script cho tât cả enemies
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
[RequireComponent(typeof(ItemDrop))]

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    // Quái vật bị choáng
    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirection;  // Hướng (x, y)
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move info")]
    public float moveSpeed; // Tốc độ
    public float idleTime; // Thời gian nhân vật Flip khi idle
    public float battleTime; // Thời gian để quái vật ignore
    private float defaultMoveSpeed; // Tốc độ di chuyển của các enemies

    [Header("Attack info")]
    public float agroDistance = 2; // Khoảng cách quái vật bắt đầu tấn công
    public float attackDistance;// KC tấn công khi áp sát người chơi
    public float attackCooldown; // Thời gian hồi chiêu 
    public float minAttackCooldown;
    public float maxAttackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }
    public EntityFX fx { get; private set; }
    private Player player;
    public string lastAnimBoolName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaultMoveSpeed = moveSpeed;
    }

    protected override void Start()
    {
        base.Start();

        fx = GetComponent<EntityFX>();
    }

    protected override void Update()
    {
        base.Update();


        stateMachine.currentState.Update();

    }

    public virtual void AssignLastAnimName(string _animBoolName) => lastAnimBoolName = _animBoolName;


    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
    }

    // Đóng băng thời gian khi thực hiện skill
    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }

    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimerCoroutine(_duration));

    // Hiệu chỉnh thời gian bị delay khi đóng băng
    protected virtual IEnumerator FreezeTimerCoroutine(float _seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }

    #region Counter Attack Window
    // Cửa sổ tấn công ô vuông đỏ của quái vật
    // Xác định quái vật nào đang chuẩn bị tấn công
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    #endregion

    // Khi cửa sổ tấn công ô vuông đỏ của quái vật
    // Giống với stunState 
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }

    // Kích hoạt anim của quái vật
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    // Check quái vật khi phát hiện người chơi
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    // Vẽ đường check phạm vi attack distance 
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}
