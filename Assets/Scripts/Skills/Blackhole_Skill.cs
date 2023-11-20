using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackhole_Skill : Skill
{
    [SerializeField] private int amountOfAttacks; // Số lần tấn công
    [SerializeField] private float cloneCooldown; // Thời gian hồi chiêu
    [SerializeField] private float blackholeDuration; // KTG blackHole
    [Space]
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private float maxSize; // Kích thước blackHole
    [SerializeField] private float growSpeed; // Tốc độ lớn của blackHole
    [SerializeField] private float shrinkSpeed; // Tốc độ thu của blackHole


    Blackhole_Skill_Controller currentBlackhole;


    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();



        GameObject newBlackHole = Instantiate(blackHolePrefab,player.transform.position,Quaternion.identity);

        currentBlackhole = newBlackHole.GetComponent<Blackhole_Skill_Controller>();

        // Truyền thuộc tính vào hàm setup 
        currentBlackhole.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldown,blackholeDuration);


        AudioManager.instance.PlaySFX(18, player.transform);
        AudioManager.instance.PlaySFX(19, player.transform);
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
    }

    // Thoát khỏi trạng thái blackHole
    public bool SkillCompleted()
    {
        if (!currentBlackhole)
            return false;


        if (currentBlackhole.playerCanExitState)
        {
            currentBlackhole = null;
            return true;
        }


        return false;
    }

    public float GetBlackholeRadius()
    {
        return maxSize / 2;
    }

}
