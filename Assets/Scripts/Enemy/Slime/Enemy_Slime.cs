using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tạo các loại slime 
public enum SlimeType { big,medium,small}

public class Enemy_Slime : Enemy
{
    [Header("Slime spesific")]
    [SerializeField] private SlimeType slimeType;
    [SerializeField] private int slimesToCreate; // Tạo ra số lượng slime
    [SerializeField] private GameObject slimePrefab;

    // Set tốc độ cho slime theo (min, max)
    [SerializeField] private Vector2 minCreationVelocity;
    [SerializeField] private Vector2 maxCreationVelocity;

    #region States

    public SlimeIdleState idleState { get ; private set; }
    public SlimeMoveState moveState { get ; private set; }
    public SlimeBattleState battleState { get ; private set; }
    public SlimeAttackState attackState { get ; private set; }
    public SlimeStunnedState stunnedState { get ; private set; }
    public SlimeDeadState deadState { get ; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // Hướng mặc định của slime là -1
        SetupDefaultFacingDir(-1);

        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        moveState = new SlimeMoveState(this, stateMachine, "Move", this);
        battleState = new SlimeBattleState(this, stateMachine, "Move", this);
        attackState = new SlimeAttackState(this, stateMachine, "Attack", this);

        stunnedState = new SlimeStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SlimeDeadState(this, stateMachine, "Idle", this);

    }


    protected override void Start()
    {
        base.Start();

        // State khởi tạo của quái vật khi bắt đầu
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        //if(Input.GetKeyDown(KeyCode.D))
        //    CreateSlimes(slimesToCreate, slimePrefab);
    }

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

        // Nếu slime = small thì clear
        if (slimeType == SlimeType.small)
            return;

        CreateSlimes(slimesToCreate, slimePrefab);

    }

    // Tạo slime theo số lượng
    private void CreateSlimes(int _amountOfSlimes, GameObject _slimePrefab)
    {
        for (int i = 0; i < _amountOfSlimes; i++)
        {
            GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);

            newSlime.GetComponent<Enemy_Slime>().SetupSlime(facingDir);
        }
    }

    public void SetupSlime(int _facingDir)
    {
        // Thay đổi hướng của slime khi di chuyển
        if (_facingDir != facingDir)
            Flip();

        // Set vận tốc di chuyển của slime theo random
        float xVelocity = Random.Range(minCreationVelocity.x, maxCreationVelocity.x);
        float yVelocity = Random.Range(minCreationVelocity.y, maxCreationVelocity.y);

        isKnocked = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * -facingDir, yVelocity);

        Invoke("CancelKnockback", 1.5f);
    }

    private void CancelKnockback() => isKnocked = false;
}
