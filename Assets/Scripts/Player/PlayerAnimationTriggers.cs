using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    // Kích hoạt trạng thái anim ngay lập tức làm ngưng các state khác
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    // Xử lý tấn công va chạm bên trong vòng tròn bán kính
    private void AttackTrigger()
    {
        AudioManager.instance.PlaySFX(2,null);

        // Xử lý các điểm anim bên trong vòng tròn này 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders) 
        {
            if (hit.GetComponent<Enemy>() != null) // kiểm tra các va chạm nằm trong bán kính
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if(_target != null) 
                    player.stats.DoDamage(_target);

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                if (weaponData != null)
                    weaponData.Effect(_target.transform);


            }
        }
    }
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
