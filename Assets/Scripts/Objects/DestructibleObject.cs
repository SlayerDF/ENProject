using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : HealthSystem
{
    private DropLootSystem lootSystem;

    protected override void Awake()
    {
        base.Awake();

        OnDiedEvent += OnDied;

        if (TryGetComponent(out DropLootSystem system))
        {
            lootSystem = system;
        }
    }

    private void OnDied(HealthSystem healthSystem)
    {
        if (lootSystem!= null)
        {
            lootSystem.DropLoot();
        }

        Destroy(gameObject);

        Debug.Log($"{gameObject.name} has been destroyed.");
    }

    private void OnDestroy()
    {
        OnDiedEvent -= OnDied;
    }
}
