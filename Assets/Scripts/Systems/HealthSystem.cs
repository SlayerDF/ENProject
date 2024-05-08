using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField, Range(1, 10)]
    private int maxHealth = 1;

    public int Health { get; private set; }

    public event Action<HealthSystem, int> OnHealedEvent;
    public event Action<HealthSystem, int> OnDamagedEvent;
    public event Action<HealthSystem> OnDiedEvent;

    private void Awake()
    {
        Health = maxHealth;
    }

    public virtual int Heal(int value)
    {
        var diff = ModifyLife(Health + value);

        if (diff == 0) return 0;

        OnHealedEvent?.Invoke(this, diff);

        Debug.Log($"[{gameObject.name}] Health value increased by {diff} points.");

        return diff;
    }

    public virtual int Damage(int value)
    {
        var diff = ModifyLife(Health - value);

        if (diff == 0) return 0;

        OnDamagedEvent?.Invoke(this, diff);

        Debug.Log($"[{gameObject.name}] Health value decreased by {diff} points.");

        if (Health <= 0)
        {
            OnDiedEvent?.Invoke(this);

            Debug.Log($"[{gameObject.name}] Health has reached zero. Object has been reported as dead.");
        }

        return Math.Abs(diff);
    }

    private int ModifyLife(int value)
    {
        value = Mathf.Clamp(value, 0, maxHealth);

        var diff = value - Health;

        Health = value;

        return diff;
    }
}
