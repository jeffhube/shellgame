using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float SPEED = 5.0f;
    private const float GROUND_RADIUS = 0.1f;
    private const float JUMP_VELOCITY = 6;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private bool _canDoubleJump = false;

    public Transform GroundCheck;
    public Transform ShellSocket;
    public SpriteRenderer ShellSpriteRenderer;
    public GameObject ShellPrefab;
    public Shell.ShellType ShellType;

    public LayerMask WhatIsGround;
    public LayerMask ShellLayer;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (ShellType == Shell.ShellType.None)
            {
                Collider2D collider = Physics2D.OverlapBox(transform.position, _boxCollider.size, 0, ShellLayer);
                if (collider != null)
                {
                    GameObject shellObject = collider.gameObject;
                    SpriteRenderer spriteRenderer = shellObject.GetComponent<SpriteRenderer>();
                    Shell shell = shellObject.GetComponent<Shell>();

                    ShellType = shell.Type;
                    ShellSpriteRenderer.sprite = spriteRenderer.sprite;

                    Destroy(shellObject);
                }
            }
            else
            {
                GameObject shellObject = Instantiate(ShellPrefab);
                shellObject.transform.position = ShellSocket.position;

                SpriteRenderer spriteRenderer = shellObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = ShellSpriteRenderer.sprite;
                Shell shell = shellObject.GetComponent<Shell>();
                shell.Type = ShellType;

                ShellType = Shell.ShellType.None;
                ShellSpriteRenderer.sprite = null;
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 40), this.ShellType.ToString());
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

        if (grounded)
        {
            _canDoubleJump = true;
            if (Input.GetButton("Jump") && newVelocity.y <= 0)
            {
                newVelocity.y = JUMP_VELOCITY;
            }
        }
        else
        {
            if (ShellType == Shell.ShellType.DoubleJump && Input.GetButtonDown("Jump") && _canDoubleJump)
            {
                _canDoubleJump = false;
                newVelocity.y = JUMP_VELOCITY;
            }
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
