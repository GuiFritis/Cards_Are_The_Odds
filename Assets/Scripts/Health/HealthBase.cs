using System;
using System.Collections;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action<HealthBase> OnDeath;
    public Action<HealthBase, float> OnDamage;
    public bool destroyOnDeath = false;
    public float baseHealth = 10f;
    [SerializeField]
    private float _curHealth;
    public float CurrentHealth {get => _curHealth;}
    private bool dead = false;

    public void ResetLife()
    {
        ResetLife(baseHealth);
    }

    public void ResetLife(float life)
    {
        _curHealth = life;
        dead = false;
        OnDamage?.Invoke(this, 0);
    }

    protected virtual void Death()
    {
        dead = true;
        OnDeath?.Invoke(this);
        if(destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        OnDamage?.Invoke(this, damage);
        if(_curHealth <= 0 && !dead)
        {
            Death();
        }
    }

    public void Die()
    {
        _curHealth = 0;
        Death();
    }
}
