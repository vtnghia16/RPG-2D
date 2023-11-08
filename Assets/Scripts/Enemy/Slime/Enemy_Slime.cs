using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public enum SlimeType { big, medium, small }

public class Enemy_Slime : Enemy
{
    [Header("Slime spesific")]
    [SerializeField] private SlimeType slimeType; // Các loại slime
    [SerializeField] private int slimesToCreate; // Slime để tạo
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Vector2 minCreationVelocity; // Vận tốc tạo tối thiểu
    [SerializeField] private Vector2 maxCreationVelocity; // Vận tốc tạo tối đa

    #region States
    public SlimeIdleState idleState { get; private set; }
    public SlimeMoveState moveState { get; private set; }
    public SlimeBattleState battleState { get; private set; }
    public SlimeAttackState attackState { get; private set; }

    public SlimeStunnedState stunnedState { get; private set; }
    public SlimeDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

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

        if (slimeType == SlimeType.small)
            return;

        CreateSlimes(slimesToCreate, slimePrefab);
    }

    // Tạo quái vật slime
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

        if (_facingDir != facingDir)
            Flip();

        // Set tốc độ (x, y) ngẫu nhiên cho slime
        float xVelocity = Random.Range(minCreationVelocity.x, maxCreationVelocity.x);
        float yVelocity = Random.Range(minCreationVelocity.y, maxCreationVelocity.y);

        isKnocked = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * -facingDir, yVelocity);

        Invoke("CancelKnockback", 1.5f);
    }

    private void CancelKnockback() => isKnocked = false;
}
