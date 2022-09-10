using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    public float _maxHealth;
    public float _health { get; protected set; }
    private bool _isDead;

    public event Action onDeath;
    protected virtual void Start()
    {
        _health = _maxHealth;
    }
    

    public virtual void TakenDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0 && _isDead == false)
        {
            Die();
        }
    }

    protected void Die()
    {
        _isDead = true;
        Destroy(gameObject);
        if (onDeath != null)
            onDeath();
    }
    
}
