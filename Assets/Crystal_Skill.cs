using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("Explosive crystal")]
    [SerializeField] private bool canExplode;


    [Header("Moving crystal")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;

    public override void UseSkill()
    {
        base.UseSkill();

        if(currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, quaternion.identity);
            Crystal_Skill_Controller currentCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

            currentCrystalScript.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed);
        }
        else
        {
            Vector2 playerPos = player.transform.position;

            player.transform.position = currentCrystal.transform.position;

            currentCrystal.transform.position = playerPos;
            currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
        }
    }
}
