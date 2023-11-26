using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeathBringerTriggers : Enemy_AnimationTriggers
{
    private Enemy_DeathBringer enemyDeathBringer => GetComponentInParent<Enemy_DeathBringer>();

    // Tìm vị trí
    private void Relocate() => enemyDeathBringer.FindPosition();

    // Ẩn/ hiện quái vật 
    private void MakeInvisible() => enemyDeathBringer.fx.MakeTransprent(true);
    private void MakeVisible() => enemyDeathBringer.fx.MakeTransprent(false);
}
