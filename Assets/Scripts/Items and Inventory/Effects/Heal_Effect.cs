using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Heal effect", menuName = "Data/Item effect/Heal effect")]
public class Heal_Effect : ItemEffect
{
    [Range(0f,1f)]
    [SerializeField] private float healPercent; // % hồi máu

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        // Lấy máu set tối đa cho nhân vật * Số % máu
        int healAmount = Mathf.RoundToInt( playerStats.GetMaxHealthValue() * healPercent);

        // + dồn vào máu chính
        playerStats.IncreaseHealthBy(healAmount);
    }
}
