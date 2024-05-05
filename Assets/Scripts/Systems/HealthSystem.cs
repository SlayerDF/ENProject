using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 1;

    private float health;

    public float Health
    {
        get => health;
        set { health = ModifyLife(value); }
    }

    public event Action<HealthSystem, float> OnHealedEvent;
    public event Action<HealthSystem, float> OnDamagedEvent;
    public event Action<HealthSystem> OnDiedEvent;

    private void Awake()
    {
        health = maxHealth;
    }

    public virtual float Heal(float value)
    {
        var diff = ModifyLife(health + value);

        if (diff == 0) return 0;

        OnHealedEvent?.Invoke(this, diff);

        Debug.Log($"[${gameObject.name}] Health value increased by ${diff} points.");

        return diff;
    }

    public virtual float Damage(float value)
    {
        var diff = ModifyLife(health - value);

        if (diff == 0) return 0;

        OnDamagedEvent?.Invoke(this, diff);

        Debug.Log($"[${gameObject.name}] Health value decreased by ${diff} points.");

        if (health == 0)
        {
            OnDiedEvent?.Invoke(this);

            Debug.Log($"[${gameObject.name}] Health has reached zero. Object has been reported as dead.");
        }

        return diff;
    }

    private float ModifyLife(float value)
    {
        value = Mathf.Clamp(health + value, 0, maxHealth);

        var diff = value - health;

        health = value;

        return diff;
    }
}
