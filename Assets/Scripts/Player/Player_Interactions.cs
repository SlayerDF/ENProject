using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [Header("Interactions")]

    [SerializeField]
    private KeyCode interactKeycode;

    [SerializeField]
    private float interactCooldownSeconds = 1f;

    private Exit exit = null;

    private float cooldownEndsAt = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Exit exit))
        {
            this.exit = exit;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Exit _))
        {
            exit = null;
        }
    }

    private void InteractionsUpdate()
    {
        if (!Input.GetKey(interactKeycode) || Time.time < cooldownEndsAt) return;

        cooldownEndsAt = Time.time + interactCooldownSeconds;

        if (exit != null) InteractWithExit(exit);
    }

    private void InteractWithExit(Exit exit)
    {
        if (exit.Opened)
        {
            Desactivate();
        }
        else if (hasShovel)
        {
            RemoveShovel();
            exit.SetState(true);
        }
    }
}
