using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Debug.Log("Player collected shovel");

            player.AddShovel();

            Destroy(gameObject);
        }
    }
}
