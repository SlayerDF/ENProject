using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
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
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Debug.Log("Player collected shovel");

            player.AddShovel();

            audioManager.SpawnTempAudioSourceAndPlay("Default", "Pickup");

            Destroy(gameObject);
        }
    }
}
