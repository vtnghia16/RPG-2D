using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{

    //[Header("Parry")]
    //[SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    //public bool parryUnlocked { get; private set; }

    [Header("Parry restore")]
    //[SerializeField] private UI_SkillTreeSlot restoreUnlockButton;
    [Range(0f,1f)]
    [SerializeField] private float restoreHealthPerentage;
    //public bool restoreUnlocked { get; private set; }

    // [Header("Parry with mirage")]
    //[SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockButton;
    //public bool parryWithMirageUnlocked { get; private set; }

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

        // parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        // restoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        // parryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }

    protected override void CheckUnlock()
    {
        // UnlockParry();
        // UnlockParryRestore();
        // UnlockParryWithMirage();
    }
    //private void UnlockParry()
    //{
    //    if (parryUnlockButton.unlocked)
    //        parryUnlocked = true;
    //}

    //private void UnlockParryRestore()
    //{
    //    if (restoreUnlockButton.unlocked)
    //        restoreUnlocked = true;
    //}

    //private void UnlockParryWithMirage()
    //{
    //    if (parryWithMirageUnlockButton.unlocked)
    //        parryWithMirageUnlocked = true;
    //}

    // Tạo nhân vật ảo khi phản đòn
    public void MakeMirageOnParry(Transform _respawnTransform)
    {
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
    }

}
