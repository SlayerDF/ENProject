using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5;

    private Rigidbody2D rb2d;

    private Vector3 moveDirection;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDirection = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0
        ).normalized;
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition((Vector3)rb2d.position + moveDirection * movementSpeed * Time.fixedDeltaTime);
    }
}
