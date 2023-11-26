using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_DeathBringer : Enemy
{
    #region States
    public DeathBringerBattleState battleState { get ; private set; }
    public DeathBringerAttackState attackState { get ; private set; }
    public DeathBringerIdleState idleState { get ; private set; }
    public DeathBringerDeadState deadState { get ; private set; }
    public DeathBringerSpellCastState spellCastState { get ; private set; }
    public DeathBringerTeleportState teleportState { get ; private set; }

    #endregion
    public bool bossFightBegun;

    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells; // Số lần castSpell
    public float spellCooldown; // Thời gian hồi chiêu
    public float lastTimeCast; // Cộng dồn thời gian khi kết thúc cast
    [SerializeField] private float spellStateCooldown;
    [SerializeField] private Vector2 spellOffset;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena; // Khu vực dịch chuyển
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport; // Cơ hội để dịch chuyển
    public float defaultChanceToTeleport = 25;

    protected override void Awake()
    {
        base.Awake();

        // Set hướng của quái vật khi tấn công
        SetupDefaultFacingDir(-1);

        idleState = new DeathBringerIdleState(this, stateMachine, "Idle", this);

        battleState = new DeathBringerBattleState(this, stateMachine, "Move", this);
        attackState = new DeathBringerAttackState(this, stateMachine, "Attack", this);

        deadState = new DeathBringerDeadState(this, stateMachine, "Idle", this);
        spellCastState = new DeathBringerSpellCastState(this, stateMachine, "SpellCast", this);
        teleportState = new DeathBringerTeleportState(this, stateMachine, "Teleport", this);
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
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);

    }

    // Sử dụng spell của quái vật
    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;

        float xOffset = 0;

        if (player.rb.velocity.x != 0)
            xOffset = player.facingDir * spellOffset.x;

        // Vị trí của spell gây sát thương lên người chơi
        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + spellOffset.y);

        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);
    }

    // Tìm vị trí của quái vật trên map
    public void FindPosition()
    {
        // Set random vị trí trong khu vực theo (x, y)
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        // Kiểm tra nếu không phải mặt đất thì không dịch chuyển
        if (!GroundBelow() || SomethingIsAround())
        {
            //Debug.Log("Tìm kiếm vị trí mới");
            FindPosition();
        }
    }


    // Check các vị trí xung quanh quái vật
    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    // Check có phải mặt đẩt
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);

    // Vẽ đường check phạm vi attack distance 
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }

    // Tính cơ hội dịch chuyển của quái vật
    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }

        return false;
    }

    // Thực hiện kỹ năng phép thuật
    public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast + spellStateCooldown)
        {
            return true;
        }

        return false;
    }
}
