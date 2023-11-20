using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill
{
    // Script skill
    public override void UseSkill()
    {
        base.UseSkill();

        CloneOnDash();
        CloneOnArrival();
    }

    protected override void Start()
    {
        base.Start();

    }

    // Tạo nhân vật ảo khi Dash
    public void CloneOnDash()
    {
        SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }

    public void CloneOnArrival()
    {
        SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }
}
