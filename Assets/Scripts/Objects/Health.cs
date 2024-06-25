using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int healthToRecover = 1;

    [Header("On destroy SFX")]

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealthSystem player))
        {
            Debug.Log("Player collected health");

            player.Heal(healthToRecover);

            audioSource.Play(audioClip);
            audioSource.Detach();

            Destroy(gameObject);
        }
    }
}
