using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed = 5;

    private Rigidbody2D rb2d;

    private Vector2 moveDirection;
    private Vector2 lastMoveDirection = new(0, -1);

    private void MovementAwake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        animator.SetFloat("MovementSpeedMultiplier", movementSpeed / 3);
    }

    private void MovementUpdate()
    {
        moveDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    private void MovementFixedUpdate()
    { 
        if (moveDirection.x == 0 && moveDirection.y == 0)
        {
            animator.SetBool("IsMoving", false);
            footstepAudioSource.Stop();

            return;
        }

        if (Vector2.Angle(lastMoveDirection, moveDirection) >= 90)
        {
            lastMoveDirection = moveDirection;
        }

        animator.SetBool("IsMoving", true);
        if (!footstepAudioSource.isPlaying) footstepAudioSource.Play();

        rb2d.MovePosition(rb2d.position + movementSpeed * Time.fixedDeltaTime * moveDirection);
    }

    private void MovementLateUpdate()
    {
        animator.SetFloat("XMovement", lastMoveDirection.x);
        animator.SetFloat("YMovement", lastMoveDirection.y);
    }
}
