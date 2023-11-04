using System;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

// Các kỹ năng của sword
public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class Sword_Skill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce info")]
    [SerializeField] private UI_SkillTreeSlot bounceUnlockButton;
    [SerializeField] private int bounceAmount;  // Số lần nảy
    [SerializeField] private float bounceGravity; // Trọng lực bounce   
    [SerializeField] private float bounceSpeed; // Tốc độ

    [Header("Peirce info")]
    [SerializeField] private UI_SkillTreeSlot pierceUnlockButton;
    [SerializeField] private int pierceAmount; 
    [SerializeField] private float pierceGravity; // Trọng lực pierce  

    [Header("Spin info")]
    [SerializeField] private UI_SkillTreeSlot spinUnlockButton;
    [SerializeField] private float hitCooldown = .35f; // Thời gian hồi chiêu
    [SerializeField] private float maxTravelDistance = 7;    // KC tối đa khi spin
    [SerializeField] private float spinDuration = 2; // KTG spin
    [SerializeField] private float spinGravity = 1; // Trọng lực

    [Header("Skill info")]
    [SerializeField] private UI_SkillTreeSlot swordUnlockButton;
    public bool swordUnlocked { get; private set; }
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce; // lực phóng (x,y)
    [SerializeField] private float swordGravity; // Trọng lực
    [SerializeField] private float freezeTimeDuration; // Thời gian đóng băng của quái vật
    [SerializeField] private float returnSpeed; // Tốc độ thanh kiếm khi trả về

    [Header("Passive skills")]
    [SerializeField] private UI_SkillTreeSlot timeStopUnlockButton;
    public bool timeStopUnlocked { get; private set; }
    [SerializeField] private UI_SkillTreeSlot vulnerableUnlockButton;
    public bool vulnerableUnlocked { get; private set; }



    private Vector2 finalDir; // Hướng mục tiêu

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots; // Số điểm trên mục tiêu
    [SerializeField] private float spaceBeetwenDots; // khoảng cách giữa các điểm
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        // Tạo các dots hướng đến mục tiêu
        GenereateDots();
        SetupGraivty();

        swordUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSword);
        bounceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBounceSword);
        pierceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockPierceSword);
        spinUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSpinSword);
        timeStopUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockTimeStop);
        vulnerableUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockVulnurable);

    }

    private void SetupGraivty()
    {
        // Lựa chọn kỹ năng
        if (swordType == SwordType.Bounce)
            swordGravity = bounceGravity;
        else if(swordType == SwordType.Pierce)
            swordGravity = pierceGravity;
        else if(swordType == SwordType.Spin)
            swordGravity = spinGravity;
    }

    protected override void Update()
    {
        // Khi KeyUp thanh kiếm sẽ đi tới mục tiêu
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        // Giữ Key để tạo ra các dot để ngắm mục tiêu
        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }

        // Chuyển đổi các sword type
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            swordType = SwordType.Regular;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            swordType = SwordType.Bounce;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            swordType = SwordType.Pierce;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            swordType = SwordType.Spin;
        }

    }

    // Tạo thanh kiếm cho nhân vật
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        // Lựa chọn các kỹ năng trong SwordType
        if (swordType == SwordType.Bounce)
            newSwordScript.SetupBounce(true, bounceAmount,bounceSpeed);
        else if (swordType == SwordType.Pierce)
            newSwordScript.SetupPierce(pierceAmount);
        else if (swordType == SwordType.Spin)
            newSwordScript.SetupSpin(true, maxTravelDistance, spinDuration,hitCooldown);

        // Các thuộc tính của thanh kiếm
        newSwordScript.SetupSword(finalDir, swordGravity, player, freezeTimeDuration, returnSpeed);

        player.AssignNewSword(newSword);

        DotsActive(false);
    }


    #region Unlock region

    protected override void CheckUnlock()
    {
        UnlockSword();
        UnlockBounceSword();
        UnlockSpinSword();
        UnlockPierceSword();
        UnlockTimeStop();
        UnlockVulnurable();
    }
    private void UnlockTimeStop()
    {
        if (timeStopUnlockButton.unlocked)
            timeStopUnlocked = true;
    }
    
    private void UnlockVulnurable()
    {
        if (vulnerableUnlockButton.unlocked)
            vulnerableUnlocked = true;
    }

    private void UnlockSword()
    {
        if (swordUnlockButton.unlocked)
        {
            swordType = SwordType.Regular;
            swordUnlocked = true;
        }
    }

    private void UnlockBounceSword()
    {
        if (bounceUnlockButton.unlocked)
            swordType = SwordType.Bounce;
    }

    private void UnlockPierceSword()
    {
        if (pierceUnlockButton.unlocked)
            swordType = SwordType.Pierce;
    }

    private void UnlockSpinSword()
    {
        if (spinUnlockButton.unlocked)
            swordType = SwordType.Spin;
    }



    #endregion

    #region Aim region
    // Hướng ngắm của thanh kiếm
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    // Tạo ra các điểm
    private void GenereateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    // Vị trí xác định các điểm theo chiều (x, y)
    private Vector2 DotsPosition(float t)
    {
        // Lực phóng
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

        return position;
    }

    #endregion
}
