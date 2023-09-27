using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength; // 1 point increase damage by 1 and crit.power by 1%
    public Stat agility; // 1 point increase evasion by 1% and crit.chance by 1%
    public Stat intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat vitality; // 1 point increase health by 3 or 5 points

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;

    public Stat damage;

    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();

    }

    public virtual void DoDamage(CharacterStats _targerStats)
    {
        if (TargetCanAvoidAttack(_targerStats))
        {
            return;
        }

        int totalDamage = damage.GetValue() + strength.GetValue();



        totalDamage = CheckTargetArmor(_targerStats, totalDamage);
        _targerStats.TakeDamage(totalDamage);
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
}
