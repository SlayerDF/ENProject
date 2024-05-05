using System;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{

    [SerializeField, Range(1f, 10f)]
    private float damageCooldown = 2f;

    private float damageCooldownExpiration = 0f;

    public event Action<HealthSystem> OnDamageCooldownStarted;
    public event Action<HealthSystem> OnDamageCooldownEnded;

    public bool DamageCooldownActive { get; private set; } = false;

    public void Update()
    {
        if (DamageCooldownActive && Time.time > damageCooldownExpiration)
        {
            DamageCooldownActive = false;

            OnDamageCooldownEnded?.Invoke(this);
        }
    }

    public override float Damage(float value)
    {
        if (Time.time < damageCooldownExpiration) return 0;

        var actualDamage = base.Damage(value);

        if (actualDamage > 0)
        {
            damageCooldownExpiration = Time.time + damageCooldown;

            OnDamageCooldownStarted?.Invoke(this);
        }

        return actualDamage;
    }
}
