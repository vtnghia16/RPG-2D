using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

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
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if(_target != null) // kiểm tra các va chạm nằm trong bán kính
                    player.stats.DoDamage(_target);

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                if (weaponData != null)
                    weaponData.Effect(_target.transform);


            }
        }
    }

    // Thực hiện kỹ năng ném kiếm
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
