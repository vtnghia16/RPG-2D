using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    private float maxSize;
    private float growSpeed; // Tốc độ lớn của vòng tròn
    private float shrinkSpeed; // Tốc độ thu nhỏ
    private float blackholeTimer;

    private bool canGrow = true;
    private bool canShrink;
    private bool canCreateHotKeys= true;
    private bool cloneAttackReleased;
    private bool playerCanDisapear = true;

    private int amountOfAttacks = 4; // Chỉ số tấn công
    private float cloneAttackCooldown = .3f; // Thời gian hồi chiêu
    private float cloneAttackTimer;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKey = new List<GameObject>();

    public bool playerCanExitState {  get; private set; }

    public void SetupBlackhole(float _maxSize, float _growSpeed, float _shrinkSpeed, int _amountOfAttacks, float _cloneAttackCooldown, float _blackholeDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;

        blackholeTimer = _blackholeDuration;


        //if (SkillManager.instance.clone.crystalInseadOfClone)
        //    playerCanDisapear = false;
    }

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;

        // Điều kiện sử dụng blackHole
        if (blackholeTimer < 0)
        {
            blackholeTimer = Mathf.Infinity;

            if (targets.Count > 0)
                ReleaseCloneAttack();
            else
                FinishBlackHoleAbility();
        }

        // Key R sử dụng blackHole
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }

        CloneAttackLogic();

        // Tăng kích thước blackHole lớn dần theo thời gian
        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        // Thu nhỏ kích thước blackHole sau khi tấn công
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            // Nếu kích thước nhỏ 0 - clear Circle
            if (transform.localScale.x < 0)
                Destroy(gameObject);
        }
    }

    // Xử lý nhân vật khi tấn công
    private void ReleaseCloneAttack()
    {
        if (targets.Count <= 0)
            return;

        DestroyHotKeys();
        cloneAttackReleased = true;
        canCreateHotKeys = false;

        // Khi nhân vật cloneAttack thì nhân vật blackHole transparent
        if (playerCanDisapear)
        {
            playerCanDisapear = false;

            PlayerManager.instance.player.fx.MakeTransprent(true);

        }
    }

    // Xử lý tạo các nhân vật ảo khi tấn công BlackHole
    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased && amountOfAttacks > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;

            if (Random.Range(0, 100) > 50)
                xOffset = 2;
            else
                xOffset = -2;

            //if (SkillManager.instance.clone.crystalInseadOfClone)
            //{
            //    SkillManager.instance.crystal.CreateCrystal();
            //    SkillManager.instance.crystal.CurrentCrystalChooseRandomTarget();
            //}
            //else
            //{
            //    SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));
            //}

            SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));

            amountOfAttacks--;

            if (amountOfAttacks <= 0)
            {
                // Độ trễ của nhân vật sau khi kết thúc blackHole 
                Invoke("FinishBlackHoleAbility", 1f);
            }
        }
    }

    // Kết thúc blackHole sau khi clone attack
    private void FinishBlackHoleAbility()
    {
        DestroyHotKeys();
        playerCanExitState = true;
        canShrink = true;
        cloneAttackReleased = false;
    }

    // Clear key khi đã thực hiện CloneAttack
    private void DestroyHotKeys()
    {
        if (createdHotKey.Count <= 0)
            return;

        for (int i = 0; i < createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }
    }

    // Xử lý các tình huống khi một đối tượng ra khỏi vùng va chạm của một đối tượng khác
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            // Làm thời gian đóng băng để xử lý các mục tiêu
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().FreezeTime(false);
    }


    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodeList.Count <= 0)
        {
            Debug.LogWarning("Không đủ hot keys trong danh sách mã khóa!");
            return;
        }

        if (!canCreateHotKeys)
            return;

        // Tạo ra các hotKey tương ứng với các nhân vật
        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKey.Add(newHotKey);

        // KeyCode được random ngẫu nhiên các phím trong keyCodeList được tạo trong GameObject
        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        Blackhole_HotKey_Controller newHotKeyScript = newHotKey.GetComponent<Blackhole_HotKey_Controller>();

        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);

}
