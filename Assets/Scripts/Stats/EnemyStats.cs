using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem; // Hệ thống rơi của vật phẩm
    public Stat soulsDropAmount;


    protected override void Start()
    {
        soulsDropAmount.SetDefaultValue(1);
        //ApplyLevelModifiers();

        base.Start();

        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();
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
