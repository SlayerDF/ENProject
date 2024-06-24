using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int healthToRecover = 1;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        audioManager.Play("Default", "Loot");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealthSystem player))
        {
            Debug.Log("Player collected health");

            player.Heal(healthToRecover);

            audioManager.SpawnTempAudioSourceAndPlay("Default", "Eat");

            Destroy(gameObject);
        }
    }
}
