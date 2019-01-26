using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float SPEED = 5.0f;
    private const float GROUND_RADIUS = 0.1f;
    private const float JUMP_VELOCITY = 6;

    private Rigidbody2D _rigidbody;

    public Transform GroundCheck;

    public LayerMask WhatIsGround;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        bool grounded = null != Physics2D.OverlapCircle(GroundCheck.position, GROUND_RADIUS, WhatIsGround);

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0)
        {
            UpdateDirection(facingRight: false);
        }
        else if (horizontal > 0)
        {
            UpdateDirection(facingRight: true);
        }

        Vector2 newVelocity = new Vector2(horizontal * SPEED, _rigidbody.velocity.y);

        if (grounded && newVelocity.y <= 0 && Input.GetButton("Jump"))
        {
            newVelocity.y = JUMP_VELOCITY;
        }

        _rigidbody.velocity = newVelocity;
    }

    private void UpdateDirection(bool facingRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = facingRight ? 1 : -1;
        transform.localScale = scale;
    }
}
