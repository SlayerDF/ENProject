using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : HealthSystem
{
    protected override void Awake()
    {
        base.Awake();

        OnDiedEvent += OnDied;
    }

    private void OnDied(HealthSystem healthSystem)
    {
        Destroy(gameObject);

        Debug.Log($"{gameObject.name} has been destroyed.");
    }

    private void OnDestroy()
    {
        OnDiedEvent -= OnDied;
    }
}
