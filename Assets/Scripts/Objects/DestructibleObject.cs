using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : HealthSystem
{
    [SerializeField]
    private Behaviour[] BehavioursToDisable;

    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private string playAudioOnDied;

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

    private async void OnDied(HealthSystem healthSystem)
    {
        if (lootSystem!= null)
        {
            lootSystem.DropLoot();
        }

        if (playAudioOnDied != null && audioManager != null)
        {
            audioManager.Play(playAudioOnDied);

            if (BehavioursToDisable != null)
            {
                for (int i = 0; i < BehavioursToDisable.Length; i++)
                {
                    BehavioursToDisable[i].enabled = false;
                }
            }

            GetComponent<SpriteRenderer>().enabled = false;

            await audioManager.WaitToFinishAll();
        }

        Destroy(gameObject);

        Debug.Log($"{gameObject.name} has been destroyed.");
    }

    private void OnDestroy()
    {
        OnDiedEvent -= OnDied;
    }
}
