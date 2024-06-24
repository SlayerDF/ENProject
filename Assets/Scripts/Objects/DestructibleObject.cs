using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : HealthSystem
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip destructionSound;

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

        if (destructionSound != null && audioSource != null)
        {
            AudioManager.SpawnTempAudioSourceAndPlay(audioSource, destructionSound);
        }

        Destroy(gameObject);

        Debug.Log($"{gameObject.name} has been destroyed.");
    }

    private void OnDestroy()
    {
        OnDiedEvent -= OnDied;
    }
}
