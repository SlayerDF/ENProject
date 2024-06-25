using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    [Header("On destroy SFX")]

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Debug.Log("Player collected shovel");

            player.AddShovel();

            audioSource.Play(audioClip);
            audioSource.Detach();

            Destroy(gameObject);
        }
    }
}
