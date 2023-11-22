using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image blackholeImage;
    [SerializeField] private Image flaskImage;

    [Header("Skill sword info")]
    [SerializeField] private Image regularImage;
    [SerializeField] private Image BounceImage;
    [SerializeField] private Image PierceImage;
    [SerializeField] private Image SpinImage;

    private SkillManager skills;


    [Header("Souls info")]
    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float soulsScoreAmount;
    //[SerializeField] private float increaseRate = 10;

    void Start()
    {
        if (playerStats != null)
            playerStats.onHealthChanged += UpdateHealthUI;

        skills = SkillManager.instance;
    }


    void Update()
    {
        UpdateSoulsUI();

        if (Input.GetKeyDown(KeyCode.LeftShift)) //&& skills.dash.dashUnlocked)
            SetCooldownOf(dashImage);

        if (Input.GetKeyDown(KeyCode.Q)) // && skills.parry.parryUnlocked
            SetCooldownOf(parryImage);

        if (Input.GetKeyDown(KeyCode.F)) // && skills.crystal.crystalUnlocked
            SetCooldownOf(crystalImage);

        if (Input.GetKeyDown(KeyCode.Mouse1)) // && skills.sword.swordUnlocked
            SetCooldownOf(swordImage);

        if (Input.GetKeyDown(KeyCode.R)) // && skills.blackhole.blackholeUnlocked
            SetCooldownOf(blackholeImage);

        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
            SetCooldownOf(flaskImage);


        // switch keycode sword
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetFillAmount(regularImage);
            SetNotFillAmount(BounceImage);
            SetNotFillAmount(PierceImage);
            SetNotFillAmount(SpinImage);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetFillAmount(BounceImage);
            SetNotFillAmount(regularImage);
            SetNotFillAmount(PierceImage);
            SetNotFillAmount(SpinImage);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetFillAmount(PierceImage);
            SetNotFillAmount(regularImage);
            SetNotFillAmount(BounceImage);
            SetNotFillAmount(SpinImage);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetFillAmount(SpinImage);
            SetNotFillAmount(regularImage);
            SetNotFillAmount(BounceImage);
            SetNotFillAmount(PierceImage);
        }

        // Check thời gian cooldown của vòng tròn
        CheckCooldownOf(dashImage, skills.dash.cooldown);
        CheckCooldownOf(parryImage, skills.parry.cooldown);
        CheckCooldownOf(crystalImage, skills.crystal.cooldown);
        CheckCooldownOf(swordImage, skills.sword.cooldown);
        CheckCooldownOf(blackholeImage, skills.blackhole.cooldown);

        CheckCooldownOf(flaskImage, Inventory.instance.flaskCooldown);

    }

    private void UpdateSoulsUI()
    {

        soulsScoreAmount = PlayerManager.instance.GetCurrency();

        currentSouls.text = ((int)soulsScoreAmount).ToString();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }

    // Set giao diện vòng tròn hồi chiêu
    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }

    private void SetFillAmount(Image _image)
    {
        _image.fillAmount = 1;
    }

    private void SetNotFillAmount(Image _image)
    {
        _image.fillAmount = 0;
    }

    // Check thời gian vòng tròn hồi chiêu
    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }


}
