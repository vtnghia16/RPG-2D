using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTriggers : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    // Kích hoạt trạng thái anim ngay lập tức làm ngưng các state khác
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    // Kích hoạt trạng thái anim ngay lập tức làm ngưng các state khác
    private void AttackTrigger()
    {
        // Xử lý các điểm anim bên trong vòng tròn này 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null) // Xử lý các điểm anim bên trong vòng tròn này 
            {
                
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);
            }
        }
    }

    // Kích hoạt event trong anim, xác định quái vật chuẩn bị tấn công
    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
