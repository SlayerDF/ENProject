using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : HealthSystem
{
    [Header("Destruction sound")]

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

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

        if (audioSource != null && audioClip != null)
        {
            audioSource.Play(audioClip);
            audioSource.Detach();
        }

        Destroy(gameObject);

        Debug.Log($"{gameObject.name} has been destroyed.");
    }

    private void OnDestroy()
    {
        OnDiedEvent -= OnDied;
    }
}
