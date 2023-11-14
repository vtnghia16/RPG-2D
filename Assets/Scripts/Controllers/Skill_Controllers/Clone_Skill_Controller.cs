using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private Player player;
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLoosingSpeed;

    private float cloneTimer;
    private float attackMultiplier;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f; // bán kính tấn công 
    private Transform closestEnemy;
    private int facingDir = 1;


    private bool canDuplicateClone;
    private float chanceToDuplicate; // Mặc định < 99%

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            // Bản sao nhân vật sẽ bị mờ dần đến biến mất khi dash
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    // Thiết lập vị trí cho bản sao nhân vật
    public void SetupClone(Transform _newTransform, float _cloneDuration, bool _canAttack, Vector3 _offset, Transform _closestEnemy, bool _canDuplicate,float _chanceToDuplicate,Player _player,float _attackMultiplier)
    {
        // Set cloneAttack theo anim AttackNumber random(1,3)
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 3));

        attackMultiplier = _attackMultiplier;
        player = _player;
        transform.position = _newTransform.position + _offset;
        cloneTimer = _cloneDuration;

        canDuplicateClone = _canDuplicate;
        chanceToDuplicate = _chanceToDuplicate;
        closestEnemy = _closestEnemy;
        FaceClosestTarget();
    }

    // Kích hoạt trạng thái anim ngay lập tức làm ngưng các state khác
    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    // Xử lý tấn công va chạm bên trong vòng tròn bán kính
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                //player.stats.DoDamage(hit.GetComponent<CharacterStats>()); 

                hit.GetComponent<Entity>().SetupKnockbackDir(transform);

                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();

                playerStats.CloneDoDamage(enemyStats, attackMultiplier);

                //if (player.skill.clone.canApplyOnHitEffect)
                //{
                //    ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                //    if (weaponData != null)
                //        weaponData.Effect(hit.transform);
                //}

                if (canDuplicateClone)
                {
                    // Set random < 99 thực hiện Duplicate attack
                    if (Random.Range(0, 100) < chanceToDuplicate)
                    {
                        SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(.5f * facingDir, 0));
                    }
                }
            }
        }
    }

    // Tấn công đối mặt với quái vật gần nhất thep hướng của nhân vật
    private void FaceClosestTarget()
    {
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
