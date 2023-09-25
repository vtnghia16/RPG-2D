using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int damage;
    public int maxHealth;

    [SerializeField] private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int _damage)
    {
        // Trừ theo % máu khi tấn công
        currentHealth -= _damage;

        if(currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
