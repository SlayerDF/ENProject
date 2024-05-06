using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed = 5;

    private Rigidbody2D rb2d;

    private Vector3 moveDirection;

    private void MovementAwake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void MovementUpdate()
    {
        moveDirection = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0
        ).normalized;
    }

    private void MovementFixedUpdate()
    {
        rb2d.MovePosition((Vector3)rb2d.position + moveDirection * movementSpeed * Time.fixedDeltaTime);
    }
}
