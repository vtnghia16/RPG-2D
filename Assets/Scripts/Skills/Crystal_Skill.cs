using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration; // KTG crystal
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("Explosive crystal")]
    [SerializeField] private float explisoveCooldown;
    [SerializeField] private bool canExplode; // Crytal nổ

    [Header("Moving crystal")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed; // Tốc độ crystal tới quái vật

    [Header("Multi stacking crystal")]
    [SerializeField] private bool canUseMultiStacks;
    [SerializeField] private int amountOfStacks; // Số Crystal
    [SerializeField] private float multiStackCooldown; // Hồi chiêu
    [SerializeField] private float useTimeWondow;
    [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();

    protected override void Start()
    {
        base.Start();

        canExplode = true;
        cooldown = explisoveCooldown;

        canMoveToEnemy = true;

        canUseMultiStacks = true;

    }


    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
            return;

        if (currentCrystal == null)
        {
            CreateCrystal();

        }
        else
        {
            if (canMoveToEnemy)
                return;

            Vector2 playerPos = player.transform.position;
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos; // Chuyển đổi vị trí crystal thành vị trí của người chơi

        }
    }

    // Khởi tạo crystal
    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        Crystal_Skill_Controller currentCystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

        currentCystalScript.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(currentCrystal.transform),player);
    }

    // Chọn mục tiêu ngẫu nhiên quái vật gần nhất để crystal
    public void CurrentCrystalChooseRandomTarget() => currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();

    // Sử dụng nhiều Crystal
    private bool CanUseMultiCrystal()
    {
        if (canUseMultiStacks)
        {
            // Kiểm tra số Crystal có trong stack
            if (crystalLeft.Count > 0)
            {
                if (crystalLeft.Count == amountOfStacks)
                    Invoke("ResetAbility", useTimeWondow);

                cooldown = 0;
                GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];
                GameObject newCrystal = Instantiate(crystalToSpawn, player.transform.position, Quaternion.identity);

                crystalLeft.Remove(crystalToSpawn); // Loại bỏ Crystal khi đã thực hiện skill

                // Set các thuộc tính khi tấn công
                newCrystal.GetComponent<Crystal_Skill_Controller>().
                    SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(newCrystal.transform),player);

                // Check không có Crystal nào trong stack
                if (crystalLeft.Count <= 0)
                {
                    cooldown = multiStackCooldown; // Set thời gian hồi chiêu
                    RefilCrystal(); // Nạp số crystal vào stack
                }


                return true;

            }
        }


        return false;
    }

    // Nạp số Crystal để thực hiện multi-crystal
    private void RefilCrystal()
    {
        int amountToAdd = amountOfStacks - crystalLeft.Count;
        for (int i = 0; i < amountToAdd; i++)
        {
            crystalLeft.Add(crystalPrefab);
        }
    }

    // Tự động nạp đầy đạn sau khoảng thời gian
    private void ResetAbility()
    {
        if (cooldownTimer > 0)
            return;

        cooldownTimer = multiStackCooldown;
        
        RefilCrystal();
    }
}
