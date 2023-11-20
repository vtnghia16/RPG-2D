using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry restore")]
    [Range(0f,1f)]
    [SerializeField] private float restoreHealthPerentage;


    public override void UseSkill()
    {
        base.UseSkill();

        // Tính % hồi máu của nhân vật
        int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthPerentage);
        player.stats.IncreaseHealthBy(restoreAmount);

    }

    protected override void Start()
    {
        base.Start();
    }

    // Tạo nhân vật ảo khi phản đòn
    public void MakeMirageOnParry(Transform _respawnTransform)
    {
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
    }

}
