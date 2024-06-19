using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool Opened { get => opened; }

    [SerializeField]
    private bool opened;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetState(opened);
    }

    public void SetState(bool opened)
    {
        this.opened = opened;

        animator.SetBool("Opened", opened);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            animator.SetBool("Selected", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent(out Player _))
        {
            animator.SetBool("Selected", false);
        }
    }
}
