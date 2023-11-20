using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private float attackMultiplier;
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration; // KTG clone
    [Space]

    [Header("Clone attack")]
    [SerializeField] private float cloneAttackMultiplier;
    [SerializeField] private bool canAttack;

    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;



    protected override void Start()
    {
        base.Start();

        canAttack = true;
        attackMultiplier = cloneAttackMultiplier;

    }

    // Tạo đối tượng tạm thời
    public void CreateClone(Transform _clonePosition,Vector3 _offset)
    {
        GameObject newClone = Instantiate(clonePrefab);

        // Set vị trí nhân vật nhân bản theo vị trí của nhân vật chính khi dash
        newClone.GetComponent<Clone_Skill_Controller>().
            SetupClone(_clonePosition, cloneDuration, canAttack,_offset,FindClosestEnemy(newClone.transform),canDuplicateClone,chanceToDuplicate,player,attackMultiplier);
    }

    public void CreateCloneWithDelay(Transform _enemyTransform)
    {
        StartCoroutine(CloneDelayCorotine(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
    }

    private IEnumerator CloneDelayCorotine(Transform _trasnform,Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
            CreateClone(_trasnform,_offset);
    }
}
