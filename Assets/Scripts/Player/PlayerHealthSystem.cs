using System;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField, Range(1f, 10f)]
    private float invulnerabilityPeriodSeconds = 2f;

    private float invulnerabilityExpiresAt = 0f;

    public event Action<HealthSystem> InvulnerabilityActivated;
    public event Action<HealthSystem> InvulnerabilityDeactivated;

    public bool IsInvulnerabilityActive { get; private set; } = false;

    private void Update()
    {
        if (IsInvulnerabilityActive && Time.time > invulnerabilityExpiresAt)
        {
            IsInvulnerabilityActive = false;

            InvulnerabilityDeactivated?.Invoke(this);

            Debug.Log($"[{gameObject.name}] temporary invulnerability has ended.");
        }
    }

    public override int Damage(int value)
    {
        if (Time.time < invulnerabilityExpiresAt) return 0;

        var actualDamage = base.Damage(value);

        if (actualDamage > 0)
        {
            invulnerabilityExpiresAt = Time.time + invulnerabilityPeriodSeconds;

            IsInvulnerabilityActive = true;

            InvulnerabilityActivated?.Invoke(this);

            Debug.Log($"[{gameObject.name}] is temporarily invulnerable.");
        }

        return actualDamage;
    }
}
