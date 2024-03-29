using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int damage;

    public event System.Action<int, int> OnHealthChanged;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public virtual void TakeDamage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int restored)
    {
        currentHealth += restored;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }
}
