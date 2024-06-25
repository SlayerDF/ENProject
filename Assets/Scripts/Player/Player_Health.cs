using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    private PlayerHealthSystem healthSystem;

    private void HealthAwake()
    {
        healthSystem = GetComponent<PlayerHealthSystem>();
        healthSystem.OnDiedEvent += OnDied;
        healthSystem.OnDamagedEvent += OnHealthValueChanged;
        healthSystem.OnHealedEvent += OnHealthValueChanged;
        healthSystem.InvulnerabilityActivated += OnInvulnerabilityActivated;
        healthSystem.InvulnerabilityDeactivated += OnInvulnerabilityDesactivated;
    }

    private void HealthOnDestroy()
    {
        healthSystem.OnDiedEvent -= OnDied;
        healthSystem.OnDamagedEvent -= OnHealthValueChanged;
        healthSystem.OnHealedEvent -= OnHealthValueChanged;
        healthSystem.InvulnerabilityActivated -= OnInvulnerabilityActivated;
        healthSystem.InvulnerabilityDeactivated -= OnInvulnerabilityDesactivated;
    }

    private void HealthStart()
    {
        gameUI.UpdateHPText(healthSystem.Health, valueIncreased: true, skipAnimation: true);
    }

    private void OnDied(HealthSystem healthSystem)
    {
        Desactivate();
        gameManager.EndGameLose().Forget();
    }

    private void OnHealthValueChanged(HealthSystem healthSystem, int value)
    {
        gameUI.UpdateHPText(healthSystem.Health, valueIncreased: value > 0);

        if (value < 0)
        {
            animator.SetTrigger("Damaged");

            healthAudioSource.Play(damagedAudioClip);

            gameManager.ApplyPlayerDamagedPenalty();
        }
    }

    private async void OnInvulnerabilityActivated(HealthSystem _)
    {
        animator.SetBool("Invulnerable", true);

        await healthAudioSource.WaitFinish();

        healthAudioSource.loop = true;
        healthAudioSource.Play(invulnerableAudioClip);
    }

    private void OnInvulnerabilityDesactivated(HealthSystem _)
    {
        animator.SetBool("Invulnerable", false);

        healthAudioSource.loop = false;
        healthAudioSource.Stop();
    }
}
