using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float lifetimeSeconds = 2f;

    [SerializeField]
    private Collider2D physicsCollider;

    void Start()
    {
        Destroy(gameObject, lifetimeSeconds);
    }

    private void OnDestroy()
    {
        Debug.Log("Bomb has been destroyed");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerMovement>()) return;

        physicsCollider.excludeLayers = 0;
    }
}
