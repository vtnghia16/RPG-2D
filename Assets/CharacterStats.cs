using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // Số liệu thống kê chính
    [Header("Major stats")]
    public Stat strength; // 1 point increase damage by 1 and crit.power by 1%
    public Stat agility; // 1 point increase evasion by 1% and crit.chance by 1%
    public Stat intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat vitality; // 1 point increase health by 3 or 5 points

    // Số liệu thống kê tấn công
    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChange;
    public Stat critPower;   // Default value 150%

    // Chỉ số phòng ngự
    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    // Chỉ số ma thuật
    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;


    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;


    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = maxHealth.GetValue();

    }

    public virtual void DoDamage(CharacterStats _targerStats)
    {
        if (TargetCanAvoidAttack(_targerStats))
        {
            return;
        }

        int totalDamage = damage.GetValue() + strength.GetValue();

        // Sát thương chí mạng
        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targerStats, totalDamage);
        // _targerStats.TakeDamage(totalDamage);
        DoMagicalDamage(_targerStats);
    }

    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        int totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        totalMagicalDamage = CheckTargetResistance(_targetStats, totalMagicalDamage);
        _targetStats.TakeDamage(totalMagicalDamage);

        if(Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
        {
            return;
        }

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while(!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if(Random.value < .3f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Applied fire");
                return;
            }

            if(Random.value < .5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Applied ice");
                return;
            }

            if(Random.value < .5f && _lightingDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Applied lighting");
                return;
            }
        }


        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);

    }

    private static int CheckTargetResistance(CharacterStats _targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {
        if(isIgnited || isChilled || isShocked) 
        {
            return;
        }

        isIgnited = _ignite;
        isChilled = _chill;
        isShocked = _shock;
    }

    public virtual void TakeDamage(int _damage)
    {
        // Trừ theo % máu khi tấn công
        currentHealth -= _damage;

        Debug.Log(_damage);

        if (currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }

    private int CheckTargetArmor(CharacterStats _targerStats, int totalDamage)
    {
        totalDamage -= _targerStats.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    // Tránh những mục tiêu khi bị Enemy tấn công
    private bool TargetCanAvoidAttack(CharacterStats _targerStats)
    {
        int totalEvasion = _targerStats.evasion.GetValue() + _targerStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }

        return false;
    }

    private bool CanCrit()
    {
        int totalCriticalChance = critChange.GetValue() + agility.GetValue();

        if(Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }

        return false;
    }

    // Tính sát thương chí mạng lên nhân vật
    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;
        float critDamage = _damage * totalCritPower;
    

        return Mathf.RoundToInt(critDamage);
    }
}
