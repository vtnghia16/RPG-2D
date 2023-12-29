using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    [Header("Level details")]
    [SerializeField] private int level = 1;
    [Range(0f, 1f)]
    [SerializeField] private float percantageModifier = .5f;

    [Space]
    private ItemDrop myDropSystem; // Hệ thống rơi của vật phẩm
    public Stat soulsDropAmount;

    [SerializeField] private int baseScore;

    protected override void Start()
    {
        soulsDropAmount.SetDefaultValue(baseScore);
        ApplyLevelModifiers();

        base.Start();

        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();
    }

    private void ApplyLevelModifiers()
    {
        Modify(damage);
        Modify(maxHealth);
        Modify(armor);

    }

    private void Modify(Stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            // CT:  _stat.GetValue() + (_stat.GetValue() * percantageModifier)
            float modifier = _stat.GetValue() * percantageModifier;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();

        PlayerManager.instance.score += soulsDropAmount.GetValue();

        // Hiển thị số điểm cuối cùng khi nhân vật die
        ScoreScript.scoreValue += soulsDropAmount.GetValue();

        // Thả vật phẩm khi quái vật die
        myDropSystem.GenerateDrop();
        Destroy(gameObject, 5f);
    }
}
