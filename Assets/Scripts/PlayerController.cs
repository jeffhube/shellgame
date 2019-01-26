using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private const float SPEED = 5.0f;
    private const float SPEEDY_SPEED = 9.0f;
    private const float JUMP_VELOCITY = 6;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private bool _canDoubleJump = false;
    private GameObject _light;

    public Transform GroundCheck;
    public Transform ShellSocket;
    public SpriteRenderer ShellSpriteRenderer;
    public GameObject ShellPrefab;
    public GameObject LightPrefab;
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
                var colliders = Physics2D.OverlapBoxAll(transform.position, _boxCollider.size, 0, ShellLayer);
                if (colliders.Length > 0)
                {
                    Collider2D closest = colliders.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First();
                    GameObject shellObject = closest.gameObject;
                    SpriteRenderer spriteRenderer = shellObject.GetComponent<SpriteRenderer>();
                    Shell shell = shellObject.GetComponent<Shell>();

                    ShellType = shell.Type;
                    ShellSpriteRenderer.sprite = spriteRenderer.sprite;

                    Destroy(shellObject);

                    if (ShellType == Shell.ShellType.Heavy)
                    {
                        _rigidbody.mass = 6;
                    }
                    else
                    {
                        _rigidbody.mass = 4;
                    }

                    if (ShellType == Shell.ShellType.Shiny)
                    {
                        _light = Instantiate(LightPrefab, ShellSocket);
                    }
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

                if (ShellType == Shell.ShellType.Heavy)
                {
                    shell.GetComponent<Rigidbody2D>().mass = 3;
                }
                if (ShellType == Shell.ShellType.Shiny)
                {
                    Instantiate(LightPrefab, shellObject.transform);
                    Destroy(_light);
                }

                ShellType = Shell.ShellType.None;
                ShellSpriteRenderer.sprite = null;
                _rigidbody.mass = 3;
            }
        }

        if (transform.position.y < -20)
        {
            Die();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 40), this.ShellType.ToString());
    }

    private void FixedUpdate()
    {
        Vector2 tl = new Vector2(transform.position.x - _boxCollider.size.x / 2, transform.position.y - _boxCollider.size.y / 2);
        Vector2 br = new Vector2(transform.position.x + _boxCollider.size.x / 2, transform.position.y - _boxCollider.size.y / 2 - 0.02f);
        var colliders = Physics2D.OverlapAreaAll(tl, br, WhatIsGround);
        bool grounded = (colliders.Any(x => x.bounds.max.y - _boxCollider.bounds.min.y < 0));

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0)
        {
            UpdateDirection(facingRight: false);
        }
        else if (horizontal > 0)
        {
            UpdateDirection(facingRight: true);
        }

        float speed = ShellType == Shell.ShellType.Speedy ? SPEEDY_SPEED : SPEED;
        Vector2 newVelocity = new Vector2(horizontal * speed, _rigidbody.velocity.y);

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
            if (ShellType == Shell.ShellType.DoubleJump && Input.GetButton("Jump") && newVelocity.y < JUMP_VELOCITY / 2 && _canDoubleJump)
            {
                _canDoubleJump = false;
                newVelocity.y = JUMP_VELOCITY;
            }
        }

        _rigidbody.velocity = newVelocity;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (ShellType == Shell.ShellType.WallBreaking && col.gameObject.GetComponent<Breakable>() != null)
        {
            col.gameObject.GetComponent<Breakable>().Break();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (ShellType != Shell.ShellType.SharkResistant && col.gameObject.GetComponent<SharkBehavior>() != null)
        {
            Die();
        }
        if (col.gameObject.GetComponent<FlagBehavior>() != null)
        {
            //TODO: Add a better win effect
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void Die()
    {
        //TODO: Add a better death effect
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateDirection(bool facingRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = facingRight ? 1 : -1;
        transform.localScale = scale;
    }
}
