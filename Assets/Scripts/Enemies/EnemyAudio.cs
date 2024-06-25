using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField]
    private HealthSystem healthSystem;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] ambientClips;

    [SerializeField]
    private AudioClip onDiedClip;

    private void Start()
    {
        healthSystem.OnDiedEvent += OnDied;
    }

    public void OnDestroy()
    {
        healthSystem.OnDiedEvent -= OnDied;
    }

    public void PlayRandomAmbient()
    {
        audioSource.Play(ambientClips[Random.Range(0, ambientClips.Length)]);
    }

    private void OnDied(HealthSystem _)
    {
        audioSource.Play(onDiedClip);
        audioSource.Detach();
    }
}
