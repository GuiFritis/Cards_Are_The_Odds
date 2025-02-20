using System;
using System.Collections;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action<HealthBase> OnDeath;
    public Action<HealthBase, int> OnDamage;
    public bool destroyOnDeath = false;
    public int baseHealth = 10;
    [SerializeField]
    private int _curHealth;
    public int CurrentHealth {get => _curHealth;}
    private bool dead = false;

    private void Awake()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        ResetLife(baseHealth);
    }

    public void ResetLife(int life)
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

    public void TakeDamage(int damage)
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
