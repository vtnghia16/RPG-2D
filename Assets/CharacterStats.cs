using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strength;
    public Stat damage;
    public Stat maxHealth;


    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();

    }

    public virtual void DoDamage(CharacterStats _targerStats)
    {
        int totalDAmage = damage.GetValue() + strength.GetValue();

        _targerStats.TakeDamage(totalDAmage);
    }

    public virtual void TakeDamage(int _damage)
    {
        // Trừ theo % máu khi tấn công
        currentHealth -= _damage;

        Debug.Log(_damage);

        if(currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
